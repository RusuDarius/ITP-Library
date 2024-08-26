using Data;
using ITPLibrary.Data.Entities;
using ITPLibrary.Data.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ITPLibrary.Data.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetPopularAndRecentlyAddedBooksAsync()
        {
            //* Added in the last 30 days
            var recentDateThreshold = DateTime.UtcNow.AddDays(-30);

            return await _context.Books
                .Where(b => b.IsPopular || b.AddedDateTime >= recentDateThreshold)
                .ToListAsync();
        }

        public async Task<Book> GetBookByIdAsync(int bookId)
        {
            return await _context.Books.FindAsync(bookId);
        }

        public async Task<IEnumerable<Book>> GetPopularBooks()
        {
            return await _context.Books.Where(b => b.IsPopular).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetPromotedBooksAsync()
        {
            return await _context.Books.Where(b => b.IsPromoted).ToListAsync();
        }

        public async Task<Book> GetBookDetailsAsync(int bookId)
        {
            return await _context.Books
                .Include(b => b.BookDetails)
                .FirstOrDefaultAsync(b => b.BookId == bookId);
        }

        public async Task<Book> AddBookAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<Book> UpdateBookAsync(Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<bool> DeleteBookAsync(int bookId)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book == null)
            {
                return false;
            }
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
