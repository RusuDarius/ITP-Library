using System.ComponentModel.DataAnnotations;
using ITPLibrary.Data.Enums;

namespace ITPLibrary.Core.Dtos.OrderDtos
{
    public class UpdateOrderDto
    {
        public int Id { get; set; }

        public AddressDto BillingAddress { get; set; }

        public AddressDto DeliveryAddress { get; set; }

        [MaxLength(200)]
        public string AdditionalInformation { get; set; }

        public PaymentTypeEnum PaymentType { get; set; }
    }
}
