using Humanizer.DateTimeHumanizeStrategy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using mvcproject.Repositories.Interfaces;
using mvcproject.Services.IService;
using mvcproject.Shared;
using SM.Data.Models.DTOs;
using SM.Data.Models.Models;

namespace mvcproject.Services
{
    public class SmartphoneService : ISmartphoneService
    {
        private readonly ISmartphoneRepository _smartphoneRepo;
        private readonly IBrandRepository _brandRepository;
        private readonly IFileService _fileService;

        public SmartphoneService(ISmartphoneRepository smartphoneRepo, IBrandRepository brandRepository, IFileService fileService)
        {
            this._smartphoneRepo = smartphoneRepo;
            this._brandRepository = brandRepository;
            this._fileService = fileService;
        }

        public async Task<IEnumerable<Smartphone>> Index()
        {
            try {
                return await _smartphoneRepo.GetSmartphones();
            }
            catch (Exception ex) {

                throw new Exception("Error " + ex);
            }
        }

        public async Task<SmartphoneDTO> AddSmartphone()
        {
            try {
                var brandSelectList = (await _brandRepository.GetBrands()).Select(brand => new SelectListItem
                {
                    Text = brand.Name,
                    Value = brand.Id.ToString(),
                });
                SmartphoneDTO brandToAdd = new() { BrandList = brandSelectList };
                return brandToAdd; 
            }
            catch(Exception ex) {
                throw new Exception("Error " + ex);
            }
            
        }

        public async Task<bool> AddSmartphoneAsync(SmartphoneDTO phoneToAdd)
        {
            try
            {

                phoneToAdd.BrandList = (await _brandRepository.GetBrands()).Select(brand => new SelectListItem
                {
                    Text = brand.Name,
                    Value = brand.Id.ToString(),
                });

                if (phoneToAdd.ImageFile != null)
                {
                    if (phoneToAdd.ImageFile.Length > 1 * 1024 * 1024)
                    {
                        return false;
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
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error " + ex);
            }
        }

        public async Task<Smartphone> GetSmartphoneById(Guid id)
        {
            try
            {
                return await _smartphoneRepo.GetSmartphoneById(id);
            }
            catch (Exception ex) {
                throw new Exception("Error " + ex);
            }
           
        }

        public async Task<SmartphoneDTO> UpdateSmartphone(Smartphone phone)
        {
            try { 
                var brandSelectList = (await _brandRepository.GetBrands()).Select(brand => new SelectListItem
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
                
                return smartphoneToUpdate;
            }
            catch(Exception ex)
            {
                throw new Exception("Error " + ex);
            }
            

            
        }

        public async Task<bool> UpdateSmartphoneAsync(SmartphoneDTO phoneToUpdate)
        {
            try
            {
                phoneToUpdate.BrandList = (await _brandRepository.GetBrands()).Select(brand => new SelectListItem
                {
                    Text = brand.Name,
                    Value = brand.Id.ToString(),
                    Selected = brand.Id == phoneToUpdate.BrandId
                });

                if (phoneToUpdate.ImageFile != null)
                {
                    if (phoneToUpdate.ImageFile.Length > 1 * 1024 * 1024)
                    {
                        return false;
                    }

                    string[] allowedExtensions = [".jpeg", ".jpg", ".png"];
                    string imageName = await _fileService.SaveFile(phoneToUpdate.ImageFile, allowedExtensions);

           
                    string oldImage = phoneToUpdate.Image;
                    phoneToUpdate.Image = imageName;

             
                    if (!string.IsNullOrWhiteSpace(oldImage))
                    {
                        _fileService.DeleteFile(oldImage);
                    }
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
                return true;
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception("Error " + ex);
            }
            catch (FileNotFoundException ex)
            {
                throw new Exception("Error " + ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error " + ex);
            }
        }

        public async Task<bool> DeleteSmartphone(Guid id)
        {
            try {
                var phone = await _smartphoneRepo.GetSmartphoneById(id);
                await _smartphoneRepo.DeleteSmartphone(phone);
                if (!string.IsNullOrWhiteSpace(phone.Image))
                {
                    _fileService.DeleteFile(phone.Image);

                }
                return true;
            }
            catch(Exception ex) {

                throw new Exception("Error " + ex);
            }
            
        }
    }


}

