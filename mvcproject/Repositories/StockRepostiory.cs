using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using mvcproject.Repositories.Interfaces;
using SM.Data;
using SM.Data.Models.DTOs;
using SM.Data.Models.Models;

namespace mvcproject.Repositories
{
    public class StockRepostiory : IStockRepostiory
    {
        private readonly ApplicationDbContext _context;

        public StockRepostiory(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Stock?> GetStockBySmartphoneId(Guid phoneId) => await _context.Stocks.FirstOrDefaultAsync(s => s.SmartphoneId == phoneId);

        public async Task ManageStock(StockDTO stockToManage)
        {
            // if there is no stock for given book id, then add new record
            // if there is already stock for given book id, update stock's quantity
            var existingStock = await GetStockBySmartphoneId(stockToManage.SmartphoneId);
            if (existingStock is null)
            {
                var stock = new Stock { SmartphoneId = stockToManage.SmartphoneId, Quantity = stockToManage.Quantity };
                _context.Stocks.Add(stock);
            }
            else
            {
                existingStock.Quantity = stockToManage.Quantity;
            }
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<StockDisplayModel>> GetStocks(string sTerm = "")
        {
            var stocks = await (from phone in _context.Smartphones
                                join stock in _context.Stocks
                                on phone.Id equals stock.SmartphoneId
                                into phone_stock
                                from phoneStock in phone_stock.DefaultIfEmpty()
                                where string.IsNullOrWhiteSpace(sTerm) || (phone.Brand.Name + phone.Name).ToLower().Contains(sTerm.ToLower())
                                select new StockDisplayModel
                                {
                                    SmartphoneId = phone.Id,
                                    SmartphoneName = phone.Name,
                                    Quantity = phoneStock == null ? 0 : phoneStock.Quantity
                                }
                                ).ToListAsync();
            return stocks;
        }


    }
}