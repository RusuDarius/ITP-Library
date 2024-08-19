using System.ComponentModel.DataAnnotations;

namespace ITPLibrary.Core.Dtos.UserDtos
{
    public class PasswordRecoveryDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
    }
}
