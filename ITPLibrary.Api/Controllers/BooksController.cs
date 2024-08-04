using Microsoft.AspNetCore.Mvc;

namespace ITPLibrary.Api.Controllers
{
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;

        public BooksController(ILogger<BooksController> logger)
        {
            _logger = logger;
        }
    }
}
