using ITPLibrary.Core.Dtos.BookDtos;

namespace ITPLibrary.Core.Services.IServices
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllBooksAsync();
        Task<IEnumerable<BookDto>> GetPopularBooksAsync();
        Task<BookDto> GetBookByIdAsync(int bookId);
        Task<BookDto> AddBookAsync(CreateBookDto createBookDto);
        Task<BookDto> UpdateBookAsync(int bookId, CreateBookDto createBookDto);
        Task<bool> DeleteBookAsync(int bookId);
    }
}
