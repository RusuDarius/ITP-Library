using ITPLibrary.Api.Constants;
using ITPLibrary.Core.Dtos.BookDetailsDtos;
using ITPLibrary.Core.Dtos.BookDtos;
using ITPLibrary.Core.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ITPLibrary.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;
        private readonly IBookService _bookService;

        public BooksController(ILogger<BooksController> logger, IBookService bookService)
        {
            _bookService = bookService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PopularAndRecentlyAddedBooksDto>>> GetAllBooks()
        {
            var books = await _bookService.GetPopularAndRecentlyAddedBooksAsync();
            return Ok(books);
        }

        [HttpGet($"{RouteConstants.Popular}")]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetPopularBooks()
        {
            var books = await _bookService.GetPopularBooksAsync();
            return Ok(books);
        }

        [HttpGet($"{RouteConstants.Promoted}")]
        public async Task<ActionResult<IEnumerable<PromotedBookDto>>> GetPromotedBooks()
        {
            var promotedBooks = await _bookService.GetPromotedBooksAsync();
            return Ok(promotedBooks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBookById(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound("Book was not found");
            }
            return Ok(book);
        }

        [HttpGet($"{RouteConstants.Details}")]
        public async Task<ActionResult<BookDetailsDto>> GetBookDetails(int id)
        {
            var book = await _bookService.GetBookDetailsAsync(id);

            if (book == null)
            {
                return NotFound("Book was not found");
            }
            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<BookDto>> AddBook(CreateBookDto createBookDto)
        {
            var book = await _bookService.AddBookAsync(createBookDto);
            return CreatedAtAction(nameof(GetBookById), new { id = book.BookId }, book);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BookDto>> UpdateBook(int id, CreateBookDto updateBookDto)
        {
            var book = await _bookService.UpdateBookAsync(id, updateBookDto);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BookDto>> RemoveBook(int id)
        {
            var book = await _bookService.DeleteBookAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
