using SM.Data.Models.DTOs;

namespace mvcproject.Services.IService
{
    public interface IHomeService
    {
        Task<DisplayPhone> Index(Guid brandId, string sterm = "");
    }
}
