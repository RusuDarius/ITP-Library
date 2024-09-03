using System.ComponentModel.DataAnnotations;
using ITPLibrary.Data.Entities;

namespace ITPLibrary.Core.Dtos.BookDtos
{
    public class BookDisplayInShoppingCartDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Title { get; set; }

        [Required]
        [MaxLength(50)]
        public string Author { get; set; }

        [Required]
        public int Price { get; set; }
        public BookDetails BookDetails { get; set; }
        public int Quantity { get; set; }
    }
}
