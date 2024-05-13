using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using mvcproject.Repositories.Interfaces;
using SM.Data;
using SM.Data.Models.Models;
using System.Runtime.InteropServices;

namespace mvcproject.Repositories
{
    

    public class UserOrderRepository : IUserOrderRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;

        public UserOrderRepository(ApplicationDbContext db, IHttpContextAccessor contextAccessor, UserManager<IdentityUser> userManager)
        {
            this._db = db;
            this._httpContextAccessor = contextAccessor;
            this._userManager = userManager;
        }

        public async Task<IEnumerable<Order>> UserOrders()
        {
            var userId = GetuserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("User is not logged in");
            }
            var orders = await _db.Orders
                            .Include(a => a.OrderStatus)
                            .Include(a => a.OrderDetail)
                            .ThenInclude(a => a.Smartphone)
                            .ThenInclude(a => a.Brand)
                            .Where(a => a.UserId == userId)
                            .ToListAsync();

            return orders;
        }

        

        private string GetuserId()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            var userId = _userManager.GetUserId(principal);
            return userId;
        }
    }
}
