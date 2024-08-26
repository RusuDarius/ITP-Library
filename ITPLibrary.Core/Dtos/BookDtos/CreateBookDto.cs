using System.ComponentModel.DataAnnotations;

namespace ITPLibrary.Core.Dtos.BookDtos
{
    public class CreateBookDto
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(50)]
        public string Author { get; set; }
        public bool IsPromoted { get; set; } = false;
        public bool IsPopular { get; set; } = false;

        [Required]
        [MaxLength(500)]
        public string? Thumbnail { get; set; }

        [Required]
        public int Price { get; set; }
    }
}
