using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using mvcproject.Repositories.Interfaces;
using mvcproject.Services.IService;
using SM.Data.Models.DTOs;




namespace mvcproject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminOperationsController : Controller
    {
        private readonly IUserOrderService _userOrderService;

        public AdminOperationsController(IUserOrderService userOrderService)
        {
            this._userOrderService = userOrderService;
        }


        public async Task<IActionResult> AllOrders()
        {
            var order = await _userOrderService.Index();
            return View(order);
        }

        public async Task<IActionResult> TogglePaymentStatus(Guid orderId)
        {
            await _userOrderService.TogglePaymentStatus(orderId);
            return RedirectToAction(nameof(AllOrders));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateOrderStatus(Guid orderId)
        {
            var data = await _userOrderService.UpdatePaymentStatus(orderId);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrderStatus(UpdateOrderStatusModel data)
        {   
            var isChanged = await _userOrderService.UpdateOrderStatus(data);

            if (isChanged)
            {
                TempData["msg"] = "Updated successfully";
            }
            else {
                TempData["msg"] = "Something went wrong";
            }

            return RedirectToAction(nameof(AllOrders), new { orderId = data.OrderStatusId });
        }

        public IActionResult Dashboard()
        {
            return View();
        }
    }

   
}

