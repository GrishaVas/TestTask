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
        {//Возвращать самый новый заказ, в котором больше одного предмета.
            var order = await _dbContext.Orders
                .Where(o => o.Quantity > 1)
                .OrderByDescending(o => o.CreatedAt)
                .FirstOrDefaultAsync();

            return order;
        }

        public async Task<List<Order>> GetOrders()
        {//Возвращать заказы от активных пользователей, отсортированные по дате создания
            var orders = await _dbContext.Orders
                .Where(o => o.User.Status == UserStatus.Active)
                .OrderBy(o => o.CreatedAt)
                .ToListAsync();

            return orders;
        }
    }
}
