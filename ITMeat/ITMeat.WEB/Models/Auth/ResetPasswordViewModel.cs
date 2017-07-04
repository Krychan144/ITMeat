using System.ComponentModel.DataAnnotations;

namespace ITMeat.WEB.Models.Auth
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string Token { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [Compare(nameof(RegisterViewModel.Password))]
        public string ConfirmPassword { get; set; }
    }
}