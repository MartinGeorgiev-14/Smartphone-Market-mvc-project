using Microsoft.CodeAnalysis.CSharp.Syntax;
using mvcproject.Repositories.Interfaces;
using mvcproject.Services.IService;
using SM.Data.Models.DTOs;
using SM.Data.Models.Models;

namespace mvcproject.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepo;

        public CartService(ICartRepository cartRepository)
        {
            this._cartRepo = cartRepository;
        }

        public async Task<int> AddItem(Guid phoneId, int quantity) {
            try {
                return await _cartRepo.AddItem(phoneId, quantity);
            }
            catch (Exception ex){
                throw new Exception("Error " + ex);
            }
        }

        public async Task RemoveItem(Guid phoneId) {
            try {
                await _cartRepo.RemoveItem(phoneId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error " + ex);
            }
        }

        public async Task<ShoppingCart> GetUserCart() {
            try
            {
                return await _cartRepo.GetUserCart();
            }
            catch (Exception ex)
            {
                throw new Exception("Error " + ex);
            }
                
        }

        public async Task<int> GetTotalItemCount()
        {
            try
            {
              return await _cartRepo.GetCartItemCount();
            }
            catch (Exception ex)
            {
                throw new Exception("Error " + ex);
            }

        }

        public async Task<bool> DoCheckout(CheckoutModel model) 
        {
            try
            {
                return await _cartRepo.DoCheckout(model);
            }
            catch (Exception ex)
            {
                throw new Exception("Error " + ex);
            }
        }
    }
}
