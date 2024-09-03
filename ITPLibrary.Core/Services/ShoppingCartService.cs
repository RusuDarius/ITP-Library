using AutoMapper;
using ITPLibrary.Core.Dtos.BookDtos;
using ITPLibrary.Core.Services.IServices;
using ITPLibrary.Data.Entities;
using ITPLibrary.Data.Repositories.IRepositories;

namespace ITPLibrary.Core.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;

        public ShoppingCartService(
            IShoppingCartRepository shoppingCartRepository,
            IBookRepository bookRepository,
            IMapper mapper
        )
        {
            _mapper = mapper;
            _shoppingCartRepository = shoppingCartRepository;
            _bookRepository = bookRepository;
        }

        public async Task<bool> AddItemToShoppingCartAsync(int userId, int bookId)
        {
            if (await _bookRepository.GetBookByIdAsync(bookId) == null)
            {
                return false;
            }

            var itemInCart = await _shoppingCartRepository.GetShoppingCartItemAsync(userId, bookId);

            if (itemInCart == null)
            {
                ShoppingCart newItemInCart = new ShoppingCart()
                {
                    BookId = bookId,
                    UserId = userId,
                    Quantity = 1
                };

                await _shoppingCartRepository.AddItemToShoppingCartAsync(newItemInCart);
            }
            else
            {
                var incrementQty = await _shoppingCartRepository.IncrementItemQuantityInCartAsync(
                    userId,
                    bookId
                );
                return incrementQty;
            }

            return true;
        }

        public async Task<bool> DeleteItemFromCartAsync(int userId, int bookId)
        {
            var itemQuantity = await _shoppingCartRepository.GetItemQuantityAsync(userId, bookId);

            if (itemQuantity == 0)
            {
                return false;
            }

            if (itemQuantity == 1)
            {
                await _shoppingCartRepository.DeleteItemFromCartAsync(userId, bookId);
            }

            if (itemQuantity > 1)
            {
                await _shoppingCartRepository.DecrementItemQuantityInCartAsync(userId, bookId);
            }

            return true;
        }

        public async Task<IEnumerable<BookDisplayInShoppingCartDto>> GetShoppingCartAsync(
            int userId
        )
        {
            var shoppingCart = await _shoppingCartRepository.GetUserShoppingcartItemsAsync(userId);
            List<BookDisplayInShoppingCartDto> shoppingCartBooks =
                new List<BookDisplayInShoppingCartDto>();

            foreach (var shoppingCartItem in shoppingCart)
            {
                var currentBook = _mapper.Map<BookDisplayInShoppingCartDto>(
                    await _bookRepository.GetBookByIdAsync(shoppingCartItem.BookId)
                );

                currentBook.Quantity = shoppingCartItem.Quantity;
                shoppingCartBooks.Add(currentBook);
            }

            return shoppingCartBooks;
        }
    }
}
