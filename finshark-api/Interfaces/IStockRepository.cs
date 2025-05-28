using finshark_api.DTOs.Stock;
using finshark_api.Helpers;
using finshark_api.Models;

namespace finshark_api.Interfaces;

public interface IStockRepository
{
    Task<List<Stock>> GetAllAsync(QueryObject query);
    Task<Stock?> GetByIdAsync(int id);
    Task<Stock> CreateAsync(Stock stockModel);
    Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto updateStockRequestDto);
    Task<Stock?> UpdateCompanyAsync(int id, UpdateCompanyNameDto updatedCompanyNameDto);
    Task<Stock?> DeleteAsync(int id);
    Task<bool> StockExists(int stockId);
}