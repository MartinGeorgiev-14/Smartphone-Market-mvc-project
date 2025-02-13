using Microsoft.AspNetCore.Mvc;
using mvcproject.Repositories.Interfaces;
using mvcproject.Services.IService;
using SM.Data.Models.DTOs;
using SM.Data.Models.Models;
using System.Runtime.CompilerServices;

namespace mvcproject.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;

        public BrandService(IBrandRepository brandRepository)
        {
            this._brandRepository = brandRepository;

        }

        public async Task<IEnumerable<Brand>> Index()
        {
            try
            {
                return await _brandRepository.GetBrands();
            }
            catch (Exception ex)
            {
                throw new Exception("Error " + ex);
            }
        }

        public async Task<bool> AddBrand(BrandDTO brand)
        {
            try
            {
                var brandToAdd = new Brand { Name = brand.BrandName, Id = brand.Id };
                await _brandRepository.AddBrand(brandToAdd);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error " + ex);
            }

        }

        public async Task<BrandDTO> UpdateBrand(Guid id)
        {
            try
            {
                var brand = await _brandRepository.GetBrandById(id);
                if (brand is null)
                    throw new InvalidOperationException($"Genre with id: {id} does not found");
                var genreToUpdate = new BrandDTO
                {
                    Id = brand.Id,
                    BrandName = brand.Name
                };
                return genreToUpdate;
            }
            catch (Exception ex)
            {
                throw new Exception("Error " + ex);
            }
        }

        public async Task<bool> DeleteBrand(Guid id)
        {
            try
            {
                var brand = await _brandRepository.GetBrandById(id);
                if (brand is null)
                {
                    throw new InvalidOperationException($"Brand with id: {id} was not found");
                }
                await _brandRepository.DeleteBrand(brand);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error " + ex);
            }
        }

        public async Task UpdateBrandPost(BrandDTO brandToUpdate)
        {
            try {
                var brand = new Brand { Name = brandToUpdate.BrandName, Id = brandToUpdate.Id };
                await _brandRepository.UpdateBrand(brand);
            }
            catch (Exception ex)
            {
                throw new Exception("Error " + ex);
            }

           
        }
    }
}
