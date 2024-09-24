using ITPLibrary.Core.Dtos.OrderDtos;

namespace ITPLibrary.Core.Services.IServices
{
    public interface IOrderService
    {
        public Task<bool> PostOrderAsync(PostOrderDto newOrder, int userId);
        public Task<IEnumerable<OrderDto>> GetAllOrdersAsync(int userId);
        public Task<bool> UpdateOrderAsync(UpdateOrderDto updatedOrder);
    }
}
