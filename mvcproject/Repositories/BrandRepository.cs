using Microsoft.Build.ObjectModelRemoting;
using Microsoft.EntityFrameworkCore;
using mvcproject.Repositories.Interfaces;
using SM.Data;
using SM.Data.Models.Models;

namespace mvcproject.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly ApplicationDbContext _context;

        public BrandRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task AddBrand(Brand brand)
        {
            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();  
        }
        
        public async Task UpdateBrand(Brand brand)
        {
            _context.Brands.Update(brand);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBrand(Brand brand)
        {
            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();
        }

        public async Task<Brand?> GetBrandById(Guid id)
        {
            return await _context.Brands.FindAsync(id);
        }

        public async Task<IEnumerable<Brand>> GetBrands()
        {
            return await _context.Brands.ToListAsync();
        }
    }
}
