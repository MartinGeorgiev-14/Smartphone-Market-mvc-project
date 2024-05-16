using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mvcproject.Repositories.Interfaces;
using SM.Data.Models.DTOs;
using SM.Data.Models.Models;
using System.Linq.Expressions;

namespace mvcproject.Controllers
{
    [Authorize(Roles = "Admin")]

    public class BrandController : Controller
    {
        private readonly IBrandRepository _brandRepo;

        public BrandController(IBrandRepository brandRepo)
        {
            this._brandRepo = brandRepo;
        }

        public async Task<IActionResult> Index()
        {
            var brands = await _brandRepo.GetBrands();
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
            try
            {
                var brandToAdd = new Brand { Name = brand.BrandName, Id = brand.Id };
                await _brandRepo.AddBrand(brandToAdd);
                TempData["successMessage"] = "Brad added successfully";
                return RedirectToAction(nameof(AddBrand));
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Brand could not be added";
                return View(brand);
            }
        }

        public async Task<IActionResult> UpdateBrand(Guid id)
        {
            var brand = await _brandRepo.GetBrandById(id);
            if (brand is null)
                throw new InvalidOperationException($"Genre with id: {id} does not found");
            var genreToUpdate = new BrandDTO
            {
                Id = brand.Id,
                BrandName = brand.Name
            };
            return View(genreToUpdate);
        }

        [HttpPost]

        public async Task<IActionResult> UpdateBrand(BrandDTO brandToUpdate)
        {
            if (!ModelState.IsValid)
            {
                return View(brandToUpdate);
            }
            try
            {
                var brand = new Brand { Name = brandToUpdate.BrandName, Id = brandToUpdate.Id }; 
                await _brandRepo.UpdateBrand(brand);
                TempData["successMessasge"] = "Brand is updated successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Brand is could not be updated";
                return View(brandToUpdate);
            }
        }

        public async Task<IActionResult> DeleteBrand(Guid id)
        {
            var brand = await _brandRepo.GetBrandById(id);
            if(brand is null)
            {
                throw new InvalidOperationException($"Brand with id: {id} was not found");
            }
            await _brandRepo.DeleteBrand(brand);
            return RedirectToAction(nameof(Index));
        }
    }
}
