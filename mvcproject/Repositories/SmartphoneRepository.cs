using Microsoft.EntityFrameworkCore;
using mvcproject.Repositories.Interfaces;
using SM.Data;
using SM.Data.Models.Models;

namespace mvcproject.Repositories
{
    public class SmartphoneRepository : ISmartphoneRepository
    {
        private readonly ApplicationDbContext _context;

        public SmartphoneRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task AddSmartphone(Smartphone smartphone)
        {
            _context.Smartphones.Add(smartphone);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSmartphone(Smartphone smartphone)
        {
            _context.Smartphones.Update(smartphone);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSmartphone(Smartphone smartphone)
        {
            _context.Smartphones.Remove(smartphone);
            await _context.SaveChangesAsync();
        }

        public async Task<Smartphone?> GetSmartphoneById(Guid id)
        {
            return await _context.Smartphones.FindAsync(id);
        }

        public async Task<IEnumerable<Smartphone>> GetSmartphones()
        {
            return await _context.Smartphones.Include(a => a.Brand).ToListAsync();
        }

    }
}
