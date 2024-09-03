using ITPLibrary.Data.Entities;

namespace ITPLibrary.Data.Repositories.IRepositories
{
    public interface IShoppingCartRepository
    {
        Task<ShoppingCart> GetShoppingCartItemAsync(int userId, int bookId);
        Task<IEnumerable<ShoppingCart>> GetUserShoppingcartItemsAsync(int userId);
        Task<int> GetItemQuantityAsync(int userId, int bookId);
        Task<bool> DeleteItemFromCartAsync(int userId, int bookId);
        Task<bool> DecrementItemQuantityInCartAsync(int userId, int bookId);
        Task<bool> IncrementItemQuantityInCartAsync(int userId, int bookId);
        Task AddItemToShoppingCartAsync(ShoppingCart item);
        Task EmptyShoppingCartAsync(int userId);
    }
}
