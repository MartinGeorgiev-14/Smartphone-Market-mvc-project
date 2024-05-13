using SM.Data.Models.Models;

namespace mvcproject.Repositories.Interfaces
{
    public interface IUserOrderRepository
    {
        Task<IEnumerable<Order>> UserOrders();
    }
}
