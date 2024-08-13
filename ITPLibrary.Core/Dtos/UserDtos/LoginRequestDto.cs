using System.ComponentModel.DataAnnotations;

namespace ITPLibrary.Core.Dtos.UserDtos
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(
            100,
            ErrorMessage = "Password must be at least 6 characters long",
            MinimumLength = 6
        )]
        public string Password { get; set; }
    }
}
