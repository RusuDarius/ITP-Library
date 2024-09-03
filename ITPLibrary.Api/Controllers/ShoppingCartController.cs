using ITPLibrary.Api.Common;
using ITPLibrary.Api.Constants;
using ITPLibrary.Core.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITPLibrary.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        [HttpGet($"{RouteConstants.GetShoppingCart}")]
        public async Task<IActionResult> GetShoppingCart()
        {
            var shoppingCart = await _shoppingCartService.GetShoppingCartAsync(
                CommonMethods.GetUserIdFromContext(HttpContext)
            );
            return Ok(shoppingCart);
        }

        [HttpPost($"{RouteConstants.AddShoppingCartItem}/{{bookId}}")]
        public async Task<ActionResult> AddShoppingCartItemAsync([FromRoute] int bookId)
        {
            var response = await _shoppingCartService.AddItemToShoppingCartAsync(
                CommonMethods.GetUserIdFromContext(HttpContext),
                bookId
            );

            if (response == false)
            {
                return BadRequest("Failed to add item in shopping cart");
            }
            return Ok("Item added to cart successfully");
        }

        [HttpDelete($"{RouteConstants.DeleteShoppingCartItem}/{{bookId}}")]
        public async Task<IActionResult> DeleteItemFromCartAsync([FromRoute] int bookId)
        {
            var response = await _shoppingCartService.DeleteItemFromCartAsync(
                CommonMethods.GetUserIdFromContext(HttpContext),
                bookId
            );

            if (response == false)
            {
                return BadRequest("Failed to delete item from shopping cart");
            }
            return Ok("Item deleted successfully");
        }
    }
}
