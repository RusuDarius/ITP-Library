using Data;
using ITPLibrary.Data.Entities;
using ITPLibrary.Data.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ITPLibrary.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Order> GetOrderAsync(int orderId)
        {
            var result = _context.Order.Where(o => o.Id == orderId).Include(o => o.BillingAddress);

            return await result.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync(int userId)
        {
            return await _context.Order
                .Where(u => u.UserId == userId)
                .Include(o => o.Items)
                .ToListAsync();
        }

        public async Task PostOrderAsync(Order newOrder)
        {
            await _context.Order.AddAsync(newOrder);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAddressAsync(Address newAddress)
        {
            _context.Address.Update(newAddress);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(Order updatedOrder)
        {
            _context.Order.Update(updatedOrder);
            await _context.SaveChangesAsync();
        }
    }
}
