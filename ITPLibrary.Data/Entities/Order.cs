using System.ComponentModel.DataAnnotations.Schema;

namespace ITPLibrary.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }

        [ForeignKey("FK_Order_UserId")]
        public int UserId { get; set; }

        [ForeignKey("FK_Order_BillingAddress")]
        public int BillingAddressId { get; set; }

        [ForeignKey("FK_Order_OrderStatus")]
        public int OrderStatusId { get; set; }

        [ForeignKey("FK_Order_PaymentType")]
        public int PaymentTypeId { get; set; }

        public DateTimeOffset DeliveryDate { get; set; }

        public Address BillingAddress { get; set; }

        public User User { get; set; }

        public int TotalPrice { get; set; }

        public List<OrderItem> Items { get; set; }
        public string AdditionalInformation { get; set; }
    }
}
