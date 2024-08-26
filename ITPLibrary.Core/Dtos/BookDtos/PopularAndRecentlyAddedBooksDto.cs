using System.ComponentModel.DataAnnotations;

namespace ITPLibrary.Core.Dtos.BookDtos
{
    public class PopularAndRecentlyAddedBooksDto
    {
        public int BookId { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Title { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Author { get; set; }

        [Required]
        public int Price { get; set; }

        [MaxLength(500)]
        public string? Thumbnail { get; set; } = string.Empty;
        public bool IsPopular { get; set; } = false;
        public bool RecentlyAdded { get; set; }
    }
}
