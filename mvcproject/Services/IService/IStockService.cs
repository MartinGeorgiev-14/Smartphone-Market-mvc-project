using Microsoft.AspNetCore.Mvc;
using SM.Data.Models.DTOs;

namespace mvcproject.Services.IService
{
    public interface IStockService
    {
        Task<IEnumerable<StockDisplayModel>> Index(string sTerm = "");
        Task<StockDTO> ManageStock(Guid phoneId);
        Task ManageStockPost(StockDTO stock);
    }
}
