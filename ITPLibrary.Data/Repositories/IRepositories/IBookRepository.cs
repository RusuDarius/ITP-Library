using ITPLibrary.Data.Entities;

namespace ITPLibrary.Data.Repositories.IRepositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetPopularAndRecentlyAddedBooksAsync();
        Task<IEnumerable<Book>> GetPopularBooks();
        Task<IEnumerable<Book>> GetPromotedBooksAsync();
        Task<Book> GetBookByIdAsync(int bookId);
        Task<Book> AddBookAsync(Book book);
        Task<Book> UpdateBookAsync(Book book);
        Task<bool> DeleteBookAsync(int bookId);
    }
}
