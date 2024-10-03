using System.ComponentModel.DataAnnotations;

namespace ITPLibrary.Core.Dtos.PaymentDtos
{
    public class CreditCardDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string CardNumber { get; set; }

        [Required]
        public int ExpirationYear { get; set; }

        [Required]
        public int ExpirationMonth { get; set; }

        [Required]
        public int CVV2 { get; set; }
    }
}
