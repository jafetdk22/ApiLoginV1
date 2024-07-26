using System.ComponentModel.DataAnnotations;

namespace ApiLogin.Models.Request.Login
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Username o Email is required.")]
        public string UsernameOrEmail { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 50 characters.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; }
    }
}
