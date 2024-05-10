using SM.Data.Models.Models;

namespace mvcproject.Repositories.Interfaces
{
    public interface IHomeRepository
    {
        Task<IEnumerable<Brand>> Brands();
        Task<IEnumerable<Smartphone>> GetPhones(Guid brandId, string sTrem = "");
    }
}
