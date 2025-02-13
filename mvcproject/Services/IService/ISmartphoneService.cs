using SM.Data.Models.DTOs;
using SM.Data.Models.Models;

namespace mvcproject.Services.IService
{
    public interface ISmartphoneService
    {
        Task<IEnumerable<Smartphone>> Index();
        Task<SmartphoneDTO> AddSmartphone();
        Task<bool> AddSmartphoneAsync(SmartphoneDTO phoneToAdd);
        Task<Smartphone> GetSmartphoneById(Guid id);
        Task<SmartphoneDTO> UpdateSmartphone(Smartphone phones);
        Task<bool> UpdateSmartphoneAsync(SmartphoneDTO phoneToUpdate);
        Task<bool> DeleteSmartphone(Guid id);
    }
}
