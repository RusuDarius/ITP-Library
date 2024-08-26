using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITPLibrary.Data.Entities
{
    public class BookDetails
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("FK_BookDetails_BookId")]
        public int BookId { get; set; }
        public string? ShortDescription { get; set; } = string.Empty;
        public string? LongDescription { get; set; } = string.Empty;
        public int Price { get; set; }
        public Book? Book { get; set; }
    }
}
