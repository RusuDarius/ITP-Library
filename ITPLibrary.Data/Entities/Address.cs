using System.ComponentModel.DataAnnotations;

namespace ITPLibrary.Data.Entities
{
    public class Address
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string AddressName { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(5)]
        public string City { get; set; }

        [Required]
        [MaxLength(15)]
        [MinLength(7)]
        public string PhoneNumber { get; set; }
    }
}
