using AutoMapper;
using ITPLibrary.Core.Dtos.BookDtos;
using ITPLibrary.Core.Services.IServices;
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

        public Task<bool> AddItemToShoppingCartAsync(int userId, int bookId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteItemFromCartAsync(int userId, int bookId)
        {
            var bookQuantity = await _shoppingCartRepository.GetItemQuantityAsync(userId, bookId);

            if (bookQuantity == 0)
            {
                return false;
            }

            if (bookQuantity == 1)
            {
                await _shoppingCartRepository.DeleteItemFromCartAsync(userId, bookId);
            }

            if (bookQuantity > 1)
            {
                await _shoppingCartRepository.DecrementItemQuantityInCartAsync(userId, bookId);
            }

            return true;
        }

        public Task<IEnumerable<BookDisplayInShoppingCartDto>> GetShoppingCart(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
