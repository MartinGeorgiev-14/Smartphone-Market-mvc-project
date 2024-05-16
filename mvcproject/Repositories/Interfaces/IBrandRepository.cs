using SM.Data.Models.Models;

namespace mvcproject.Repositories.Interfaces
{
    public interface IBrandRepository
    {
        Task AddBrand(Brand brand);
        Task UpdateBrand(Brand brand);
        Task<Brand?> GetBrandById(Guid id);
        Task DeleteBrand(Brand brand);
        Task<IEnumerable<Brand>> GetBrands();
    }
}
