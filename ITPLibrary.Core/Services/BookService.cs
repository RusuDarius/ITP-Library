using AutoMapper;
using ITPLibrary.Core.Dtos.BookDtos;
using ITPLibrary.Core.Services.IServices;
using ITPLibrary.Data.Entities;
using ITPLibrary.Data.Repositories.IRepositories;

namespace ITPLibrary.Core.Services
{
    public class BookService : IBookService
    {
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;

        public BookService(IMapper mapper, IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllBooksAsync();
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<BookDto> GetBookByIdAsync(int bookId)
        {
            var book = await _bookRepository.GetBookByIdAsync(bookId);
            return _mapper.Map<BookDto>(book);
        }

        public async Task<IEnumerable<BookDto>> GetPopularBooksAsync()
        {
            var books = await _bookRepository.GetPopularBooks();
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<IEnumerable<PromotedBookDto>> GetPromotedBooksAsync()
        {
            var promotedBooks = await _bookRepository.GetPromotedBooksAsync();
            return _mapper.Map<IEnumerable<PromotedBookDto>>(promotedBooks);
        }

        public async Task<BookDto> AddBookAsync(CreateBookDto createBookDto)
        {
            var book = _mapper.Map<Book>(createBookDto);
            book = await _bookRepository.AddBookAsync(book);
            return _mapper.Map<BookDto>(book);
        }

        public async Task<BookDto> UpdateBookAsync(int bookId, CreateBookDto createBookDto)
        {
            var book = await _bookRepository.GetBookByIdAsync(bookId);
            if (book == null)
            {
                return null;
            }

            _mapper.Map(createBookDto, book);
            book = await _bookRepository.UpdateBookAsync(book);
            return _mapper.Map<BookDto>(book);
        }

        public async Task<bool> DeleteBookAsync(int bookId)
        {
            return await _bookRepository.DeleteBookAsync(bookId);
        }
    }
}
