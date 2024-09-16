using ITPLibrary.Data.Entities;

namespace ITPLibrary.Data.Repositories.IRepositories
{
    public interface IOrderItemRepository
    {
        public Task<bool> PostOrderItem(OrderItem item);
    }
}
