using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mvcproject.Repositories.Interfaces;
using mvcproject.Services.IService;
using SM.Data.Models.DTOs;

namespace mvcproject.Controllers
{
    //[Authorize]


    public class CartController : Controller
    {
        ICartService _cartService;

        public CartController(ICartService cartService)
        {
            this._cartService = cartService;
        }


        public async Task<IActionResult> AddItem(Guid phoneId, int quantity = 1, int redirect = 0)
        {
            var cartCount = await _cartService.AddItem(phoneId, quantity);
            if(redirect == 0)
            {
                return Ok(cartCount);
            }
            else
            {
                return RedirectToAction("GetUserCart");
            }
        }

        public async Task<IActionResult> RemoveItem(Guid phoneId)
        {
            await _cartService.RemoveItem(phoneId);
            return RedirectToAction("GetUserCart");
        }

        public async Task<IActionResult> GetUserCart()
        {
            var cart = await _cartService.GetUserCart();
            return View(cart);
        }

        public async Task<IActionResult> GetTotalItemInCart()
        {
            int cartItem = await _cartService.GetTotalItemCount();
            return Ok(cartItem);
        }

        public IActionResult Checkout() { 
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutModel model)
        {
       
            bool isCheckedOut = await _cartService.DoCheckout(model);
            if (!isCheckedOut)
            {
                return RedirectToAction(nameof(OrderFail));
            }
            return RedirectToAction(nameof(OrderSuccess));

        }

        public IActionResult OrderSuccess()
        {
            return View();
        }

        public IActionResult OrderFail()
        {
            return View();
        }

    }
}
