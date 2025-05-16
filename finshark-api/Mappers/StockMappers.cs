using finshark_api.DTOs.Stock;
using finshark_api.Models;

namespace finshark_api.Mappers;

public static class StockMappers
{
    // renvoi un stockDTO mais avec un filtrage ou on choisit la donnée qu'on veut envoyer dans la structure
    public static StockDto ToStockDto(this Stock stockModel)
    {
        return new StockDto()
        {
            Id = stockModel.Id,
            Symbol = stockModel.Symbol,
            CompanyName = stockModel.CompanyName,
            Industry = stockModel.Industry
        };
    }
}