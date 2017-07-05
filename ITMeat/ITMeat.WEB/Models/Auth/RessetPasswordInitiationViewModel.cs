using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITMeat.WEB.Models.Auth
{
    public class ResetPasswordInitiationViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Please enter the email address you provided when registering.")]
        public string Email { get; set; }
    }
}