using ITPLibrary.Api.Common;
using ITPLibrary.Api.Constants;
using ITPLibrary.Core.Dtos.OrderDtos;
using ITPLibrary.Core.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ITPLibrary.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost(RouteConstants.PostOrder)]
        public async Task<IActionResult> PostOrder(PostOrderDto order)
        {
            int userId = CommonMethods.GetUserIdFromContext(HttpContext);
            if (await _orderService.PostOrderAsync(order, userId) == false)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpGet(RouteConstants.GetOrders)]
        public async Task<IActionResult> GetOrders()
        {
            int userId = CommonMethods.GetUserIdFromContext(HttpContext);
            var orders = await _orderService.GetAllOrdersAsync(userId);

            if (orders == null)
            {
                return BadRequest();
            }

            return Ok(orders);
        }

        [HttpPut(RouteConstants.UpdateOrder)]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderDto order)
        {
            var updateResponse = await _orderService.UpdateOrderAsync(order);
            if (updateResponse == false)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
