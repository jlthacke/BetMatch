using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetMatch.Models.Wager
{
    public class WagerModel
    {
        public int Id { get; set; }

        [Display(Name="Your Team")]
        [Required(ErrorMessage = "* Required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string TeamName_For { get; set; }
        
        [Display(Name = "Opponent's Team")]
        [Required(ErrorMessage = "* Required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string TeamName_Against { get; set; }
        
        [Display(Name = "Spread, MoneyLine")]
        [Required(ErrorMessage = "* Required")]
        [Range(-1000, 1000, ErrorMessage = "Value must be between -1000 and 1000")]
        public int Spread { get; set; }
        
        [Display(Name = "Game Time")]
        [Required(ErrorMessage = "* Required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string GameTime { get; set; }
        
        [Display(Name = "Wager")]
        [Required(ErrorMessage = "* Required")]
        [Range(1, 1000, ErrorMessage = "Value must be between 1 and 1000")]
        public int Wager { get; set; }
        
        public DateTime CreateDate { get; set; }
        public string CreateUser { get; set; }
        public string CreateUser_FullName { get; set; }
        public DateTime? AcceptDate { get; set; }
        public string AcceptUser { get; set; }
        public string AcceptUser_FullName { get; set; }
        public bool HasCreatorSettled { get; set; }
        public bool HasAcceptorSettled { get; set; }
    }
}