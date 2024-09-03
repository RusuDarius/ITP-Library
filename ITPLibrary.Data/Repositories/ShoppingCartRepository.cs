using Data;
using ITPLibrary.Data.Entities;
using ITPLibrary.Data.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ITPLibrary.Data.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly AppDbContext _context;

        public ShoppingCartRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ShoppingCart> GetShoppingCartItemAsync(int userId, int bookId)
        {
            var bookInCart = await _context.ShoppingCart
                .Where(u => u.UserId == userId && u.BookId == bookId)
                .FirstOrDefaultAsync();
            return bookInCart!;
        }

        public async Task<IEnumerable<ShoppingCart>> GetUserShoppingcartItemsAsync(int userId)
        {
            var shoppingCart = await _context.ShoppingCart
                .Where(u => u.UserId == userId)
                .Include(u => u.Book)
                .ToListAsync();

            return shoppingCart;
        }

        public async Task<int> GetItemQuantityAsync(int userId, int bookId)
        {
            var shoppingCartItem = await GetShoppingCartItemAsync(userId, bookId);
            return shoppingCartItem == null ? 0 : shoppingCartItem.Quantity;
        }

        public async Task AddItemToShoppingCartAsync(ShoppingCart item)
        {
            _context.ShoppingCart.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteItemFromCartAsync(int userId, int bookId)
        {
            var shoppingCartItem = await GetShoppingCartItemAsync(userId, bookId);
            if (shoppingCartItem == null)
            {
                return false;
            }

            _context.ShoppingCart.Remove(shoppingCartItem);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DecrementItemQuantityInCartAsync(int userId, int bookId)
        {
            var itemInCart = await _context.ShoppingCart
                .Where(u => u.UserId == userId && u.BookId == bookId)
                .FirstOrDefaultAsync();

            if (itemInCart == null)
            {
                return false;
            }

            itemInCart.Quantity--;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IncrementItemQuantityInCartAsync(int userId, int bookId)
        {
            var itemInCart = await _context.ShoppingCart
                .Where(u => u.UserId == userId && u.BookId == bookId)
                .FirstOrDefaultAsync();

            if (itemInCart == null)
            {
                return false;
            }

            itemInCart.Quantity++;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task EmptyShoppingCartAsync(int userId)
        {
            var shoppingCart = await _context.ShoppingCart
                .Where(u => u.UserId == userId)
                .ToListAsync();

            foreach (var item in shoppingCart)
            {
                _context.ShoppingCart.Remove(item);
            }

            await _context.SaveChangesAsync();
        }
    }
}
