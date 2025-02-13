using SM.Data.Models.DTOs;
using SM.Data.Models.Models;

namespace mvcproject.Services.IService
{
    public interface IUserOrderService
    {
        Task<IEnumerable<Order>> Index();
        Task TogglePaymentStatus(Guid orderId);
        Task<UpdateOrderStatusModel> UpdatePaymentStatus(Guid orderId);
        Task<bool> UpdateOrderStatus(UpdateOrderStatusModel data);
    }
}
