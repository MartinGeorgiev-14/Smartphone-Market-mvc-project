using Microsoft.AspNetCore.Mvc;
using mvcproject.Repositories;
using mvcproject.Repositories.Interfaces;
using mvcproject.Services.IService;
using SM.Data.Models.DTOs;
using SM.Data.Models.Models;

namespace mvcproject.Services
{
    public class StockService : IStockService
    {
        public IStockRepostiory _stockRepostiory { get; set; }

        public StockService(IStockRepostiory stockRepostiory)
        {
            this._stockRepostiory = stockRepostiory;
        }

        public async Task<IEnumerable<StockDisplayModel>> Index(string sTerm = "")
        {
            try { 
                return await _stockRepostiory.GetStocks(sTerm);
            }
            catch (Exception ex) {
                throw new Exception("Error " + ex);
            }
            
        }

        public async Task<StockDTO> ManageStock(Guid phoneId)
        {
            try
            {
                var existingStock = await _stockRepostiory.GetStockBySmartphoneId(phoneId);

                var stock = new StockDTO
                {
                    SmartphoneId = phoneId,
                    Quantity = existingStock != null ? existingStock.Quantity : 0,
                };
                return stock;
            }
            catch (Exception ex)
            {
                throw new Exception("Error " + ex);
            }

        }

        public async Task ManageStockPost(StockDTO stock) 
        {
            try
            {
                await _stockRepostiory.ManageStock(stock);
            }catch (Exception ex)
            {
                throw new Exception("Error " + ex);
            }
        }

    }
}


