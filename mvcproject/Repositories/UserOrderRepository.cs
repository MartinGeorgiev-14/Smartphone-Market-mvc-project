using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using mvcproject.Repositories.Interfaces;
using SM.Data;
using SM.Data.Models.DTOs;
using SM.Data.Models.Models;
using System.Runtime.InteropServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public async Task ChangeOrderStatus(UpdateOrderStatusModel data)
        {
            var order = await _db.Orders.FindAsync(data.Id);
            if(order == null)
            {
                throw new InvalidOperationException($"Order with id: {data.Id} was not found");
            }
            order.OrderStatusId = data.OrderStatusId;
            await _db.SaveChangesAsync();
        }

        public async Task<Order?> GetOrderById(Guid id)
        {
            return await _db.Orders.FindAsync(id);
        }

        public async Task<IEnumerable<OrderStatus>> GetOrderStatuses()
        {
            return await _db.OrderStatuses.ToListAsync();
        }

        public async Task TogglePaymentStatus(Guid orderId)
        {
            var order = await _db.Orders.FindAsync(orderId);
            if (order == null)
            {
                throw new InvalidOperationException($"Order with id: {orderId} was not found");
            }
            order.isPaid = !order.isPaid;
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Order>> UserOrders(bool getAll = false)
        {
            var orders = _db.Orders
                            .Include(a => a.OrderStatus)
                            .Include(a => a.OrderDetail)
                            .ThenInclude(a => a.Smartphone)
                            .ThenInclude(a => a.Brand).AsQueryable();

            if(!getAll) 
            {
                var userId = GetuserId();
                if (string.IsNullOrEmpty(userId))
                {
                    throw new Exception("User is not logged in");
                }

                orders = orders.Where(a => a.UserId == userId);
                return await orders.ToListAsync();
            }

            return await orders.ToListAsync();
        }

        private string GetuserId()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            var userId = _userManager.GetUserId(principal);
            return userId;
        }
    }
}
