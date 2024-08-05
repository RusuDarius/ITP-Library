namespace ITPLibrary.Core.Dtos.BookDtos
{
    public class BookDto
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Price { get; set; }
        public bool IsPopular { get; set; }
    }
}
