using Microsoft.AspNetCore.Mvc;
using mvcproject.Models;
using mvcproject.Repositories.Interfaces;
using mvcproject.Services.IService;
using SM.Data.Models.DTOs;
using SM.Data.Models.Models;
using System.Diagnostics;

namespace mvcproject.Controllers
{
    public class HomeController : Controller
    {

        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            this._homeService = homeService;
        }

        public async Task<IActionResult> Index(Guid brandId, string sterm = "")
        {
            var phoneModel = await _homeService.Index(brandId, sterm);
            return View(phoneModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
