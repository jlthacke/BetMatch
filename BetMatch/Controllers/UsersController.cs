using System.Collections.Generic;
using System.Web.Mvc;
using BetMatch.DAL.User;
using BetMatch.Models.User;
using BetMatch.Models.Enums;

namespace BetMatch.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly UserRepository userRepo = new UserRepository();
        
        public ActionResult UserList()
        {
            // Load Users
            List<UserModel> users = userRepo.GetUsers((int)UserGetQuery.ListAll_AdminApproved);

            return View("ListUsers", users);
        }
    }
}