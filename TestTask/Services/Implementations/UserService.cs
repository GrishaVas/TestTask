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
        {
            var user = await _dbContext.Users
                .OrderByDescending(u => u.Orders.Where(o => o.CreatedAt.Year == 2003).Sum(o => o.Price))
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("User does not exist!");
            }

            return user;
        }

        public async Task<List<User>> GetUsers()
        {
            var users = await _dbContext.Users
                .Include(u => u.Orders)
                .Where(u => u.Orders.Any(o => o.CreatedAt.Year == 2010 && o.Status == OrderStatus.Paid))
                .ToListAsync();

            return users;
        }
    }
}
