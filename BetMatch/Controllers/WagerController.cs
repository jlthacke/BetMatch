using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetMatch.Models.Wager;
using BetMatch.Models.Enums;
using BetMatch.DAL.Wager;
using Microsoft.AspNet.Identity;

namespace BetMatch.Controllers
{
    [Authorize]
    public class WagerController : Controller
    {
        private readonly WagerRepository wagerRepo = new WagerRepository();

        public ActionResult BetFeed()
        {
            // Load wagers
            List<WagerModel> wagers = wagerRepo.GetWagers((int)WagerGetQuery.BetFeed, User.Identity.GetUserName());

            return View("BetFeed", wagers);
        }

        public ActionResult OpenBets()
        {
            ViewBag.Wagers = wagerRepo.GetWagers((int)WagerGetQuery.OpenBets, User.Identity.GetUserName());
            return View("OpenBets",new WagerModel());
        }

        public ActionResult PendingSettlement()
        {
            List<WagerModel> wagers = wagerRepo.GetWagers((int)WagerGetQuery.PendingSettlement, User.Identity.GetUserName());
            return View("PendingSettlement",wagers);
        }

        public ActionResult SettledBets()
        {
            List<WagerModel> wagers = wagerRepo.GetWagers((int)WagerGetQuery.SettledWagers, User.Identity.GetUserName());
            return View("SettledBets",wagers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddWager()
        {
            // Load the model
            WagerModel wager = new WagerModel();
            TryUpdateModel<WagerModel>(wager);

            // Check if model is valid
            if (!ModelState.IsValid)
            {
                // Load bets and store in viewbag
                ViewBag.Wagers = wagerRepo.GetWagers((int)WagerGetQuery.OpenBets, User.Identity.GetUserName());
                wager.Id = -1;

                // Redirect back to bet feed with invalid model
                return View("OpenBets", wager);
            }

            // Pass model to database for insert
            wagerRepo.EditWager((int)WagerEditQuery.Insert, wager, User.Identity.GetUserName());

            // Refresh page
            return OpenBets();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AcceptWager(int WagerId)
        {
            // Create model
            WagerModel wager = new WagerModel
            {
                Id = WagerId
            };

            // Pass model to database for update
            wagerRepo.EditWager((int)WagerEditQuery.Accept, wager, User.Identity.GetUserName());

            // Refresh page
            return BetFeed();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SettleWager(int WagerId)
        {
            // Create model
            WagerModel wager = new WagerModel
            {
                Id = WagerId
            };

            // Pass model to database for update
            wagerRepo.EditWager((int)WagerEditQuery.Settle, wager, User.Identity.GetUserName());

            // Refresh page
            return PendingSettlement();
        }
    }
}