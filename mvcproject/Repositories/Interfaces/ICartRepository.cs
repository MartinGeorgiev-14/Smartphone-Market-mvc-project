using SM.Data.Models.DTOs;
using SM.Data.Models.Models;

namespace mvcproject.Repositories.Interfaces
{
    public interface ICartRepository
    {
        Task<int> AddItem(Guid phoneId, int quantity);
        Task<int> RemoveItem(Guid phoneId);
        Task<ShoppingCart> GetUserCart();
        Task<int> GetCartItemCount(string userId = "");
        Task<ShoppingCart> GetCart(string userId);
        Task<bool> DoCheckout(CheckoutModel model);
    }
}
