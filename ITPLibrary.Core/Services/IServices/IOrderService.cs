using ITPLibrary.Core.Dtos.OrderDtos;

namespace ITPLibrary.Core.Services.IServices
{
    public interface IOrderService
    {
        public Task<bool> PostOrder(PostOrderDto newOrder, int userId);
        public Task<IEnumerable<OrderDto>> GetAllOrders(int userId);
        public Task<bool> UpdateOrder(UpdateOrderDto updatedOrder);
    }
}
