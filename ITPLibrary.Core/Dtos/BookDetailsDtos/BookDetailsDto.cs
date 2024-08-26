using System.ComponentModel.DataAnnotations;

namespace ITPLibrary.Core.Dtos.BookDetailsDtos
{
    public class BookDetailsDto
    {
        [Key]
        public int BookId { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Title { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Author { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(500)]
        public string? LongDescription { get; set; }
        public string? Image { get; set; }
    }
}
