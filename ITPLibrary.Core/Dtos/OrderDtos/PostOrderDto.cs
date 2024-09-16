using System.ComponentModel.DataAnnotations;

namespace ITPLibrary.Core.Dtos.OrderDtos
{
    public class PostOrderDto
    {
        [Required]
        public AddressDto BillingAddress { get; set; }

        [Required]
        public string PaymentType { get; set; }

        public DateTimeOffset DeliveryDate { get; set; }

        [MaxLength(200)]
        public string AdditionalInformation { get; set; }
    }
}
