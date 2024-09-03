using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITPLibrary.Data.Entities
{
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("FK_ShoppingCart_BookId")]
        public int BookId { get; set; }

        [ForeignKey("FK_ShoppingCart_UserId")]
        public int UserId { get; set; }
        public Book? Book { get; set; }
        public User? User { get; set; }
    }
}
