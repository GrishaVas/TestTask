using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Enums;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class UserService : IUserService
    {
        private ApplicationDbContext _dbContext;

        public UserService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetUser()
        {//Возвращать пользователя с максимальной общей суммой товаров, доставленных в 2003
            var user = await _dbContext.Users
                .OrderByDescending(u => u.Orders.Where(o => o.CreatedAt.Year == 2003 && o.Status == OrderStatus.Delivered).Sum(o => o.Price))
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<List<User>> GetUsers()
        {//Возвращать пользователей у которых есть оплаченные заказы в 2010
            var users = await _dbContext.Users
                .Where(u => u.Orders.Any(o => o.CreatedAt.Year == 2010 && o.Status == OrderStatus.Paid))
                .ToListAsync();

            return users;
        }
    }
}
