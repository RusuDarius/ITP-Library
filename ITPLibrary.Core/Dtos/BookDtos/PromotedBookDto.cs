using System.ComponentModel.DataAnnotations;

namespace ITPLibrary.Core.Dtos.BookDtos
{
    public class PromotedBookDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string? Thumbnail { get; set; }

        [MaxLength(200)]
        public string ShortDescription { get; set; }
    }
}
