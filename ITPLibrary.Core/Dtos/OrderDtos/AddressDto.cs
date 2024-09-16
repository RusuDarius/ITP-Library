using System.ComponentModel.DataAnnotations;

namespace ITPLibrary.Core.Dtos.OrderDtos
{
    public class AddressDto
    {
        [Required]
        public string City { get; set; }

        [Required]
        public string AddressName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }
}
