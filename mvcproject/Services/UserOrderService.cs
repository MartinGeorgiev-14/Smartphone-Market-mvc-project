using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using mvcproject.Repositories;
using mvcproject.Repositories.Interfaces;
using mvcproject.Services.IService;
using SM.Data.Models.DTOs;
using SM.Data.Models.Models;

namespace mvcproject.Services
{
    public class UserOrderService : IUserOrderService
    {
        private readonly IUserOrderRepository _userOrderRepo;

        public UserOrderService(IUserOrderRepository userOrderRepository)
        {
            this._userOrderRepo = userOrderRepository;
        }

        public async Task<IEnumerable<Order>> Index() {

            try
            {
                return await _userOrderRepo.UserOrders(true);

            } catch (Exception ex)
            {
                throw new Exception("Error " + ex);
            }

        }

        public async Task TogglePaymentStatus(Guid orderId) {
            try
            {
                await _userOrderRepo.TogglePaymentStatus(orderId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error " + ex);
            }
        }

        public async Task<UpdateOrderStatusModel> UpdatePaymentStatus(Guid orderId) {
            try {
                var order = await _userOrderRepo.GetOrderById(orderId);
                if (order == null)
                {
                    throw new InvalidOperationException($"Order with id: {orderId} was not found");
                }

                var orderStatusList = (await _userOrderRepo.GetOrderStatuses()).Select(orderStatus =>
                {
                    return new SelectListItem { Value = orderStatus.Id.ToString(), Text = orderStatus.StatusName, Selected = order.OrderStatusId == orderStatus.Id };
                });

                var data = new UpdateOrderStatusModel
                {
                    OrderId = orderId,
                    OrderStatusId = order.OrderStatusId,
                    OrderStatusList = orderStatusList
                };

                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("Error " + ex); throw new Exception("Error " + ex);
            }
        }

        public async Task<bool> UpdateOrderStatus(UpdateOrderStatusModel data) {
            try
            {
                await _userOrderRepo.ChangeOrderStatus(data);
                return true;
            }
            catch (Exception ex){
                throw new Exception("Error: " + ex);
            }
        }
    }
}
