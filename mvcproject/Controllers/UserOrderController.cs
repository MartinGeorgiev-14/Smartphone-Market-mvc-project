using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mvcproject.Repositories.Interfaces;
using mvcproject.Services.IService;

namespace mvcproject.Controllers
{
    [Authorize]
    public class UserOrderController : Controller
    {
        private readonly IUserOrderService _userOrderService;

        public UserOrderController(IUserOrderService userOrderService)
        {
            this._userOrderService = userOrderService;
        }


        public async Task<IActionResult> UserOrders()
        {
            var orders = await _userOrderService.Index();
            return View(orders);
        }
    }
}
