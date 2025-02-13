using SM.Data.Models.DTOs;
using SM.Data.Models.Models;

namespace mvcproject.Services.IService
{
    public interface IBrandService
    {
        Task<IEnumerable<Brand>> Index();
        Task<bool> AddBrand(BrandDTO brand);
        Task<BrandDTO> UpdateBrand(Guid id);
        Task<bool> DeleteBrand(Guid id);
        Task UpdateBrandPost(BrandDTO brandToUpdate);
    }
}
