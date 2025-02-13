using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mvcproject.Repositories.Interfaces;
using mvcproject.Services.IService;
using SM.Data.Models.DTOs;

namespace mvcproject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StockController : Controller
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            this._stockService = stockService;
        }
        // Retunrs stock page
        public async Task<IActionResult> Index(string sTerm = "")
        {
            var stocks = await _stockService.Index(sTerm);

            return View(stocks);
        }

        // returns manage stock page
        public async Task<IActionResult> ManageStock(Guid phoneId)
        {
            var stock = await _stockService.ManageStock(phoneId);

            return View(stock);
        }

        // changes stock quantity of a smartphone
        [HttpPost]
        public async Task<IActionResult> ManageStock(StockDTO stock)
        {          
            await _stockService.ManageStockPost(stock);
            return RedirectToAction(nameof(Index));
        }
    }
}
