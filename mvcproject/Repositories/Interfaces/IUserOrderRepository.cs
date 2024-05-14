using SM.Data.Models.DTOs;
using SM.Data.Models.Models;

namespace mvcproject.Repositories.Interfaces
{
    public interface IUserOrderRepository
    {
        Task<IEnumerable<Order>> UserOrders(bool getAll = false);

        Task ChangeOrderStatus(UpdateOrderStatusModel data);

        Task TogglePaymentStatus(Guid orderId);

        Task<Order?> GetOrderById(Guid id);

        Task<IEnumerable<OrderStatus>> GetOrderStatuses();
    }
}
