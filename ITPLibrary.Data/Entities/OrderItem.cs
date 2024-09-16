using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITPLibrary.Data.Entities
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("FK_Order_OrderItem")]
        public int OrderId { get; set; }

        [ForeignKey("FK_Book_OrderItem")]
        public int BookId { get; set; }

        public int Price { get; set; }

        public int Quantity { get; set; }

        public Book Book { get; set; }

        public Order Order { get; set; }
    }
}
