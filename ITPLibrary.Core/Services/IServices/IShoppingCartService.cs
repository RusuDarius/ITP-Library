using ITPLibrary.Core.Dtos.BookDtos;

namespace ITPLibrary.Core.Services.IServices
{
    public interface IShoppingCartService
    {
        Task<bool> DeleteItemFromCartAsync(int userId, int bookId);
        Task<bool> AddItemToShoppingCartAsync(int userId, int bookId);
        Task<IEnumerable<BookDisplayInShoppingCartDto>> GetShoppingCartAsync(int userId);
    }
}
