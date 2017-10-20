using System.ComponentModel.DataAnnotations;

namespace BetMatch.Models.User
{
    public class UserModel
    {
        public string UserName { get; set; }

        [Display(Name = "Name")]
        public string FullName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}