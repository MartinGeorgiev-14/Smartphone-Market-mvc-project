using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using mvcproject.Repositories.Interfaces;
using mvcproject.Shared;
using SM.Data.Models.DTOs;
using SM.Data.Models.Models;
using System.Drawing.Drawing2D;

namespace mvcproject.Controllers
{
    [Authorize(Roles = "Admin")]

    public class SmartphoneController : Controller
    {
        private readonly ISmartphoneRepository _smartphoneRepo;
        private readonly IBrandRepository _brandRepo;
        private readonly IFileService _fileService;

        public SmartphoneController(ISmartphoneRepository smartphoneRepo, IBrandRepository brandRepo, IFileService fileService)
        {
            this._smartphoneRepo = smartphoneRepo;
            this._brandRepo = brandRepo;
            this._fileService = fileService;
        }

        public async Task<IActionResult> Index()
        {
            var phones = await _smartphoneRepo.GetSmartphones();
            return View(phones);
        }

        public async Task<IActionResult> AddSmartphone()
        {
            var brandSelectList = (await _brandRepo.GetBrands()).Select(brand => new SelectListItem
            {
                Text = brand.Name,
                Value = brand.Id.ToString(),
            });
            SmartphoneDTO brandToAdd = new() { BrandList = brandSelectList };
            return View(brandToAdd);
        }

        [HttpPost]
        public async Task<IActionResult> AddSmartphone(SmartphoneDTO phoneToAdd)
        {
            var brandSelectList = (await _brandRepo.GetBrands()).Select(brand => new SelectListItem
            {
                Text = brand.Name,
                Value = brand.Id.ToString(),
            });
            phoneToAdd.BrandList = brandSelectList;

            if (!ModelState.IsValid)
            {
                return View(phoneToAdd);
            }
                

            try
            {
                if (phoneToAdd.ImageFile != null)
                {
                    if (phoneToAdd.ImageFile.Length > 1 * 1024 * 1024)
                    {
                        throw new InvalidOperationException("Image file can not exceed 1 MB");
                    }
                    string[] allowedExtensions = [".jpeg", ".jpg", ".png"];
                    string imageName = await _fileService.SaveFile(phoneToAdd.ImageFile, allowedExtensions);
                    phoneToAdd.Image = imageName;
                }
               
                Smartphone smartphone = new()
                {
                    Id = phoneToAdd.Id,
                    Name = phoneToAdd.SmartphoneName,
                    Image = phoneToAdd.Image,
                    BrandId = phoneToAdd.BrandId,
                    Price = phoneToAdd.Price,
                    ShortDescription = phoneToAdd.ShortDescription,
                    LongDescription = phoneToAdd.LongDescription,
                };
                await _smartphoneRepo.AddSmartphone(smartphone);
                TempData["successMessage"] = "Smartphone is added successfully";
                return RedirectToAction(nameof(AddSmartphone));
            }
            catch (InvalidOperationException ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(phoneToAdd);
            }
            catch (FileNotFoundException ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(phoneToAdd);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Error on saving data";
                return View(phoneToAdd);
            }
        }

        public async Task<IActionResult> UpdateSmartphone(Guid id)
        {
            var phone = await _smartphoneRepo.GetSmartphoneById(id);
            if (phone == null)
            {
                TempData["errorMessage"] = $"Smartphone with the id: {id} does not found";
                return RedirectToAction(nameof(Index));
            }
            var brandSelectList = (await _brandRepo.GetBrands()).Select(brand => new SelectListItem
            {
                Text = brand.Name,
                Value = brand.Id.ToString(),
                Selected = brand.Id == phone.BrandId
            });
            SmartphoneDTO smartphoneToUpdate = new()
            {
                BrandList = brandSelectList,
                SmartphoneName = phone.Name,
                BrandId = phone.BrandId,
                Price = phone.Price,
                Image = phone.Image,
                ShortDescription = phone.ShortDescription,
                LongDescription = phone.LongDescription,
            };
            return View(smartphoneToUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSmartphone(SmartphoneDTO phoneToUpdate)
        {
            var brandSelectList = (await _brandRepo.GetBrands()).Select(brand => new SelectListItem
            {
                Text = brand.Name,
                Value = brand.Id.ToString(),
                Selected = brand.Id == phoneToUpdate.BrandId
            });
            phoneToUpdate.BrandList = brandSelectList;

            if (!ModelState.IsValid)
                return View(phoneToUpdate);

            try
            {
                string oldImage = "";
                if (phoneToUpdate.ImageFile != null)
                {
                    if (phoneToUpdate.ImageFile.Length > 1 * 1024 * 1024)
                    {
                        throw new InvalidOperationException("Image file can not exceed 1 MB");
                    }
                    string[] allowedExtensions = [".jpeg", ".jpg", ".png"];
                    string imageName = await _fileService.SaveFile(phoneToUpdate.ImageFile, allowedExtensions);
                    // hold the old image name. Because we will delete this image after updating the new
                    oldImage = phoneToUpdate.Image;
                    phoneToUpdate.Image = imageName;
                }
     
                Smartphone smartphone = new()
                {
                    Id = phoneToUpdate.Id,
                    Name = phoneToUpdate.SmartphoneName,
                    BrandId = phoneToUpdate.BrandId,
                    Price = phoneToUpdate.Price,
                    Image = phoneToUpdate.Image,
                    ShortDescription = phoneToUpdate.ShortDescription,
                    LongDescription = phoneToUpdate.LongDescription,
                };
                await _smartphoneRepo.UpdateSmartphone(smartphone);
                // if image is updated, then delete it from the folder too
                if (!string.IsNullOrWhiteSpace(oldImage))
                {
                    _fileService.DeleteFile(oldImage);
                }
                TempData["successMessage"] = "Smartphone is updated successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(phoneToUpdate);
            }
            catch (FileNotFoundException ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(phoneToUpdate);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Error on saving data";
                return View(phoneToUpdate);
            }
        }

        public async Task<IActionResult> DeleteSmartphone(Guid id)
        {
            try
            {
                var phone = await _smartphoneRepo.GetSmartphoneById(id);
                if (phone == null)
                {
                    TempData["errorMessage"] = $"Smartphone with the id: {id} does not found";
                }
                else
                {
                    await _smartphoneRepo.DeleteSmartphone(phone);
                    if (!string.IsNullOrWhiteSpace(phone.Image))
                    {
                        _fileService.DeleteFile(phone.Image);
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            catch (FileNotFoundException ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Error on deleting the data";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
