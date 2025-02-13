using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mvcproject.Repositories.Interfaces;
using mvcproject.Services.IService;
using SM.Data.Models.DTOs;
using SM.Data.Models.Models;
using System.Linq.Expressions;

namespace mvcproject.Controllers
{
    [Authorize(Roles = "Admin")]

    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            this._brandService = brandService;
        }

        public async Task<IActionResult> Index()
        {
            var brands = await _brandService.Index();
            return View(brands);
        }

        public IActionResult AddBrand()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBrand(BrandDTO brand)
        {
            if (!ModelState.IsValid)
            {
                return View(brand);
            }
           
            bool isAdded = await _brandService.AddBrand(brand);

            if (isAdded)
            {
                TempData["successMessage"] = "Brand added successfully";
                return RedirectToAction(nameof(AddBrand));
            }
            else
            {
                TempData["errorMessage"] = "Brand could not be added";
                return View(brand);
            }
        }

        public async Task<IActionResult> UpdateBrand(Guid id)
        {
            var genreToUpdate = await _brandService.UpdateBrand(id);
           
            return View(genreToUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBrand(BrandDTO brandToUpdate)
        {
                await _brandService.UpdateBrandPost(brandToUpdate);
                return RedirectToAction(nameof(Index));    
        }

        public async Task<IActionResult> DeleteBrand(Guid id)
        {
            bool IsDeleted = await _brandService.DeleteBrand(id);

            if (IsDeleted)
            {
                return RedirectToAction(nameof(Index));
            }
            else {
                throw new InvalidOperationException($"Brand with id: {id} was not found");
            }
            
        }
    }
}
