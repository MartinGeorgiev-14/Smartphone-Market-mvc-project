using SM.Data.Models.DTOs;
using SM.Data.Models.Models;

namespace mvcproject.Services.IService
{
    public interface ICartService
    {
        Task<int> AddItem(Guid phoneId, int quantity);
        Task RemoveItem(Guid phoneId);
        Task<ShoppingCart> GetUserCart();
        Task<int> GetTotalItemCount();
        Task<bool> DoCheckout(CheckoutModel model);
    }
}
