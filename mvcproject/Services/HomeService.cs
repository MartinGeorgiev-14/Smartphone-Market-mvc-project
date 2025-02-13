using mvcproject.Repositories;
using mvcproject.Repositories.Interfaces;
using mvcproject.Services.IService;
using SM.Data.Models.DTOs;
using SM.Data.Models.Models;
using System.IO;

namespace mvcproject.Services
{
    public class HomeService : IHomeService
    {
        private readonly IHomeRepository _homeRepo;

        public HomeService(IHomeRepository homeRepository)
        {
            this._homeRepo = homeRepository;
        }

        public async Task<DisplayPhone> Index(Guid brandId, string sterm = "") {

            try {
                IEnumerable<Smartphone> phones = await _homeRepo.GetPhones(brandId, sterm);
                IEnumerable<Brand> brands = await _homeRepo.Brands();
                DisplayPhone phoneModel = new DisplayPhone
                {
                    Smartphones = phones,
                    Brands = brands,
                    STerm = sterm,
                    BrandId = brandId
                };

                return phoneModel;
            }
            catch (Exception ex) {
                throw new Exception("Error " + ex);
            }
            
        }
    }
}
