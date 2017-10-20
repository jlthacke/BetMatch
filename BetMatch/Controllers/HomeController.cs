using System.Web.Mvc;
using BetMatch.Extensions;

namespace BetMatch.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            bool isAuthen = User.Identity.IsAuthenticated;
            bool isAdminAppr = User.Identity.IsAdminApproved();

            // Render view based on status
            if(isAdminAppr) {
                return View("Main_AdminApproved");
            } else if(isAuthen) {
                return View("Main_IsAuthenticated");
            } else {
                return View("Main_Unregistered");
            }
        }
    }
}