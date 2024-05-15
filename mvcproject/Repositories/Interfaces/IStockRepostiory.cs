using SM.Data.Models.DTOs;
using SM.Data.Models.Models;

namespace mvcproject.Repositories.Interfaces
{
    public interface IStockRepostiory
    {
        Task<IEnumerable<StockDisplayModel>> GetStocks(string sTerm = "");
        Task<Stock?> GetStockBySmartphoneId(Guid bookId);
        Task ManageStock(StockDTO stockToManage);
    }
}
