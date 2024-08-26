using System.ComponentModel.DataAnnotations;

namespace ITPLibrary.Core.Dtos.BookDtos
{
    public class BookDto
    {
        public int BookId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(50)]
        public string Author { get; set; }
        public int Price { get; set; }
        public bool IsPopular { get; set; }
    }
}
