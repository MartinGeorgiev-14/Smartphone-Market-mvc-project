using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mvcproject.Repositories.Interfaces;
using SM.Data.Models.DTOs;

namespace mvcproject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StockController : Controller
    {
        private readonly IStockRepostiory _stockRepo;

        public StockController(IStockRepostiory stockRepo)
        {
            this._stockRepo = stockRepo;
        }

        public async Task<IActionResult> Index(string sTerm = "")
        {
            var stocks = await _stockRepo.GetStocks(sTerm);

            return View(stocks);
        }

        public async Task<IActionResult> ManageStock(Guid phoneId)
        {
            var existingStock = await _stockRepo.GetStockBySmartphoneId(phoneId);

            var stock = new StockDTO
            {
                SmartphoneId = phoneId,
                Quantity = existingStock != null ? existingStock.Quantity : 0,
            };
            return View(stock);
        }

        [HttpPost]
        public async Task<IActionResult> ManageStock(StockDTO stock)
        {
            if(!ModelState.IsValid) 
            {
                return View(stock);
                
            }

            try
            {
                await _stockRepo.ManageStock(stock);
                TempData["successMessage"] = "Stock has been updated successfully";
            }
            catch (Exception ex) 
            {
                TempData["errorMessage"] = "Something went wrong";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
