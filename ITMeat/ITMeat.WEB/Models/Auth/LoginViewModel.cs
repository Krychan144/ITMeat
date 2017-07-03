﻿using System.ComponentModel.DataAnnotations;

namespace ITMeat.WEB.Models.Auth
{
    public class LoginViewModel
    {
        [Required]
        public string DomainName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}