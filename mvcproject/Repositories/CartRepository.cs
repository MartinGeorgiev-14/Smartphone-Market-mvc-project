﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using mvcproject.Repositories.Interfaces;
using NuGet.Protocol;
using SM.Common;
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
        private readonly OrderStatusConstants _orderStatusConstants;

        public CartRepository(ApplicationDbContext db, UserManager<IdentityUser> userManeger, IHttpContextAccessor httpContextAccessor)
        {
            this._db = db;
            this._userManager = userManeger;
            this._httpContextAccessor = httpContextAccessor;
            this._orderStatusConstants = new OrderStatusConstants();
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

        public async Task<bool> DoCheckout()
        {
            using var transaction = _db.Database.BeginTransaction();

            try 
            {
                var userId = GetuserId();
                if (string.IsNullOrEmpty(userId))
                {
                    throw new Exception("User is not logged in");
                }
                
                var cart = await GetCart(userId);
                if (cart == null)
                {
                    throw new Exception("Ivalid cart");
                }

                var cartDetail = _db.CartDetails
                                        .Where(a => a.ShoppingCartId == cart.Id).ToList();

                if(cartDetail.Count == 0)
                {
                    throw new Exception("Cart is empty");
                }

                var pendingRecord = _db.OrderStatuses.FirstOrDefault(s => s.StatusName == "Pending");
                if (pendingRecord is null)
                    throw new InvalidOperationException("Order status does not have Pending status");

                var order = new Order
                {
                    UserId = userId,
                    CreateDate = DateTime.Now,
                    OrderStatusId = pendingRecord.Id
                };

                _db.Orders.Add(order);
                _db.SaveChanges();


                foreach (var item in cartDetail)
                {
                    var orderDetail = new OrderDetail
                    {
                        SmartphoneId = item.SmartphoneId,
                        OrderId = order.Id,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice,
                    };
                    _db.OrderDetails.Add(orderDetail);
                }
                _db.SaveChanges();

                // removing cart details
                _db.CartDetails.RemoveRange(cartDetail);
                _db.SaveChanges();

                transaction.Commit();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
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
