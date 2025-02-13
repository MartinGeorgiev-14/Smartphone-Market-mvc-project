using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using mvcproject.Repositories.Interfaces;
using mvcproject.Services.IService;
using mvcproject.Shared;
using SM.Data.Models.DTOs;
using SM.Data.Models.Models;
using System.Drawing.Drawing2D;

namespace mvcproject.Controllers
{
    [Authorize(Roles = "Admin")]

    public class SmartphoneController : Controller
    {

        private readonly IFileService _fileService;
        private readonly ISmartphoneService _smartphoneService;

        public SmartphoneController(IFileService fileService, ISmartphoneService smartphoneService)
        {
            this._fileService = fileService;
            this._smartphoneService = smartphoneService;
        }

        //Returns Smartphones page
        public async Task<IActionResult> Index()
        {
            var phones = await _smartphoneService.Index();
            return View(phones);
        }

        //Returns Add more smartphones page
        public async Task<IActionResult> AddSmartphone()
        {
            SmartphoneDTO brandToAdd = await _smartphoneService.AddSmartphone();
            return View(brandToAdd);
        }

        //Adds a new smartphone to db
        [HttpPost]
        public async Task<IActionResult> AddSmartphone(SmartphoneDTO phoneToAdd)
        {
            if (!ModelState.IsValid)
            {
                return View(phoneToAdd);
            }

            var result = await _smartphoneService.AddSmartphoneAsync(phoneToAdd);

            if (result)
            {
                TempData["successMessage"] = "Smartphone has been added succesfuly";
                return RedirectToAction(nameof(AddSmartphone));
            }
            else
            {
                TempData["errorMessage"] = "Error adding smartphone";
                return View(phoneToAdd);
            }
        }

        //Returns Edit smartphone page with a certain smartphone
        public async Task<IActionResult> UpdateSmartphone(Guid id)
        {
            var phone = await _smartphoneService.GetSmartphoneById(id);
            if (phone == null)
            {
                TempData["errorMessage"] = $"Smartphone with the id: {id} does not found";
                return RedirectToAction(nameof(Index));
            }
            SmartphoneDTO smartphoneToUpdate = await _smartphoneService.UpdateSmartphone(phone);
            return View(smartphoneToUpdate);
        }

        //Edits a certain smartphone
        [HttpPost]
        public async Task<IActionResult> UpdateSmartphone(SmartphoneDTO phoneToUpdate)
        {
            if (!ModelState.IsValid)
            {
                return View(phoneToUpdate);
            }

            var result = await _smartphoneService.UpdateSmartphoneAsync(phoneToUpdate);

            if (result)
            {
                TempData["successMessage"] = "Smartphone has been updated succesfuly";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["errorMessage"] = "Error updating smartphone";
                return View(phoneToUpdate);
            }
        }

        //Deletes a certain smartphone from db
        public async Task<IActionResult> DeleteSmartphone(Guid id)
        {
            var isDeleted = await _smartphoneService.DeleteSmartphone(id);

                if (!isDeleted)
                {
                    TempData["errorMessage"] = $"Smartphone with the id: {id} was not found";
                }

            return RedirectToAction(nameof(Index));
        }
    }
}
