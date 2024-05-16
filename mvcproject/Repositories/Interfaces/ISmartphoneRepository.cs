using SM.Data.Models.Models;

namespace mvcproject.Repositories.Interfaces
{
    public interface ISmartphoneRepository
    {
        Task AddSmartphone(Smartphone smartphone);

        Task DeleteSmartphone(Smartphone smartphone);

        Task<Smartphone?> GetSmartphoneById(Guid id);
        
        Task<IEnumerable<Smartphone>> GetSmartphones();

        Task UpdateSmartphone(Smartphone smartphone);
    }
}
