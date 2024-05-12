using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using mvcproject.Repositories.Interfaces;
using SM.Data;
using SM.Data.Models.Models;
using System.Security.Claims;

namespace mvcproject.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartRepository(ApplicationDbContext db, UserManager<IdentityUser> userManeger, IHttpContextAccessor httpContextAccessor)
        {
            this._db = db;
            this._userManager = userManeger;
            this._httpContextAccessor = httpContextAccessor;
        }

        public async Task<int> AddItem(Guid phoneId, int quantity)
        {
            string userId = GetuserId();
            using var transaction = _db.Database.BeginTransaction();
            try
            {
               
                if (string.IsNullOrEmpty(userId))
                {
                    throw new Exception("Invalid userId");
                }

                var cart = await GetCart(userId);
                if (cart is null)
                {
                    cart = new ShoppingCart
                    {
                        UserId = userId
                    };
                    _db.ShoppingCarts.Add(cart);
                }

                _db.SaveChanges();
                // cart detail section
                var cartItem = _db.CartDetails.FirstOrDefault(c => c.ShoppingCartId == cart.Id && c.SmartphoneId == phoneId);
                if (cartItem is not null)
                {
                    cartItem.Quantity += quantity;
                }
                else
                {
                    var phone = _db.Smartphones.Find(phoneId);
                    cartItem = new CartDetail
                    {
                        SmartphoneId = phoneId,
                        ShoppingCartId = cart.Id,
                        Quantity = quantity,
                        UnitPrice = phone.Price
                    };
                    _db.CartDetails.Add(cartItem);
                }
                _db.SaveChanges();
                transaction.Commit();
                
            }
            catch (Exception ex)
            {
                
            }
            var cartItemCount = await GetCartItemCount(userId);
            return cartItemCount;
        }


        public async Task<int> RemoveItem(Guid phoneId)
        {
            string userId = GetuserId();
            using var transaction = _db.Database.BeginTransaction();
            try
            {
               
                if (string.IsNullOrEmpty(userId))
                {
                    throw new Exception("Ivalid userId");
                }

                var cart = await GetCart(userId);
                if (cart is null)
                {
                    throw new Exception("Cart is empty");
                }
                // cart detail section
                var cartItem = _db.CartDetails.FirstOrDefault(c => c.ShoppingCartId == cart.Id && c.SmartphoneId == phoneId);
                if (cartItem is null)
                {
                    throw new Exception("No items in the cart");
                }
                else if (cartItem.Quantity == 1)
                {
                    _db.CartDetails.Remove(cartItem);
                }
                else
                {
                    cartItem.Quantity = cartItem.Quantity - 1;
                }
                _db.SaveChanges();

               
            }
            catch (Exception ex)
            {

            }
            var cartItemCount = await GetCartItemCount(userId);
            return cartItemCount;

        }

        public async Task<ShoppingCart> GetUserCart()
        {
            var userId = GetuserId();
            if(userId == null)
            {
                throw new Exception("Invalid user");
            }
            var shoppingCart = await _db.ShoppingCarts
                                  .Include(a => a.CartDetails)
                                  .ThenInclude(a => a.Smartphone)
                                  .ThenInclude(a => a.Stock)
                                  .Include(a => a.CartDetails)
                                  .ThenInclude(a => a.Smartphone)
                                  .ThenInclude(a => a.Brand)
                                  .Where(a => a.UserId == userId).FirstOrDefaultAsync();

            return shoppingCart;
        }

        public async Task<int> GetCartItemCount(string userId="")
        {
            if (string.IsNullOrEmpty(userId))
            {
                userId = GetuserId();
            }
            var data = await (from cart in _db.ShoppingCarts
                              join cartDetail in _db.CartDetails
                              on cart.Id equals cartDetail.ShoppingCartId
                              where cart.UserId == userId
                              select new { cartDetail.Id }
                              ).ToListAsync();
            return data.Count;
        }

        private string GetuserId()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            var userId = _userManager.GetUserId(principal);
            return userId;
        }

        public async Task<ShoppingCart> GetCart(string userId)
        {
            var cart = await _db.ShoppingCarts.FirstOrDefaultAsync(x => x.UserId == userId);
            return cart;
        }
    }
}
