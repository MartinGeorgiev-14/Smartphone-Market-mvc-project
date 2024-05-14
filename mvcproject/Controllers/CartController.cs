using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mvcproject.Repositories.Interfaces;
using SM.Data.Models.DTOs;

namespace mvcproject.Controllers
{
    //[Authorize]


    public class CartController : Controller
    {
        ICartRepository _cartRepo;

        public CartController(ICartRepository cartRepo)
        {
            this._cartRepo = cartRepo;
        }


        public async Task<IActionResult> AddItem(Guid phoneId, int quantity = 1, int redirect = 0)
        {
            var cartCount = await _cartRepo.AddItem(phoneId, quantity);
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
            var cartCount = await _cartRepo.RemoveItem(phoneId);

            return RedirectToAction("GetUserCart");
        }

        public async Task<IActionResult> GetUserCart()
        {
            var cart = await _cartRepo.GetUserCart();
            return View(cart);
        }

        public async Task<IActionResult> GetTotalItemInCart()
        {
            int cartItem = await _cartRepo.GetCartItemCount();
            return Ok(cartItem);
        }

        public IActionResult Checkout() { 
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool isCheckedOut = await _cartRepo.DoCheckout(model);
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
