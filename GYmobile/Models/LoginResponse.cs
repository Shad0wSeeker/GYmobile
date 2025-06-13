
using System.ComponentModel.DataAnnotations;

namespace GYmobile.Models
{
    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string UserRole {  get; set; } = string.Empty;
    }

    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password {  get; set; } = string.Empty;

        [Compare("Password", ErrorMessage = "Пароли не совпадают.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public bool IsLandlord { get; set; }
    }

}
