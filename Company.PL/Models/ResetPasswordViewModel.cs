using System.ComponentModel.DataAnnotations;

namespace Company.PL.Models
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Minimum password length is 5")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirmed Password is required")]
        [Compare(nameof(Password), ErrorMessage = "Password is n`t ")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
