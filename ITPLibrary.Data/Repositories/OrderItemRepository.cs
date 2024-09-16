using Data;
using ITPLibrary.Data.Entities;

namespace ITPLibrary.Data.Repositories
{
    public class OrderItemRepository
    {
        private readonly AppDbContext _context;

        public OrderItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> PostOrderItem(OrderItem item)
        {
            if (item == null)
            {
                return false;
            }

            await _context.OrderItem.AddAsync(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
