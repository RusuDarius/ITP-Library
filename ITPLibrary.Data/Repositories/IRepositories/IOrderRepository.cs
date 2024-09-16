using ITPLibrary.Data.Entities;

namespace ITPLibrary.Data.Repositories.IRepositories
{
    public interface IOrderRepository
    {
        public Task PostOrderAsync(Order newOrder);
        public Task<IEnumerable<Order>> GetOrdersAsync(int userId);
        public Task UpdateOrderAsync(Order updatedOrder);
        public Task<Order> GetOrderAsync(int orderId);
        public Task UpdateAddressAsync(Address newAddress);
    }
}
