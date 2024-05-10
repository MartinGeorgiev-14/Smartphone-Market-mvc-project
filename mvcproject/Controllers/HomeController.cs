using Microsoft.AspNetCore.Mvc;
using mvcproject.Models;
using mvcproject.Repositories.Interfaces;
using SM.Data.Models.DTOs;
using SM.Data.Models.Models;
using System.Diagnostics;

namespace mvcproject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeRepository _homeRepository;

        public HomeController(ILogger<HomeController> logger, IHomeRepository homeRepository)
        {
            this._logger = logger;
            this._homeRepository = homeRepository;
        }

        public async Task<IActionResult> Index(Guid brandId, string sterm = "")
        {

            IEnumerable<Smartphone> phones = await _homeRepository.GetPhones(brandId, sterm);
            IEnumerable<Brand> brands = await _homeRepository.Brands();
            DisplayPhone phoneModel = new DisplayPhone 
            {
                Smartphones = phones,
                Brands = brands,
                STerm = sterm,
                BrandId = brandId
            };

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
