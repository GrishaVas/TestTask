using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Enums;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private ApplicationDbContext _dbContext;

        public OrderService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Order> GetOrder()
        {
            var order = await _dbContext.Orders
                .Where(o => o.Quantity > 1)
                .OrderByDescending(o => o.CreatedAt)
                .FirstOrDefaultAsync();

            if (order == null)
            {
                throw new ArgumentException("Order does not exist!");
            }

            return order;
        }

        public async Task<List<Order>> GetOrders()
        {
            var orders = await _dbContext.Orders
                .Where(o => o.User.Status == UserStatus.Active)
                .OrderBy(o => o.CreatedAt)
                .ToListAsync();

            return orders;
        }
    }
}
