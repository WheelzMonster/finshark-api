using finshark_api.Data;
using finshark_api.DTOs.Stock;
using finshark_api.Helpers;
using finshark_api.Interfaces;
using finshark_api.Mappers;
using finshark_api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace finshark_api.Repositories;

public class StockRepository : IStockRepository
{
    private readonly ApplicationDBContext _dbContext;
    
    public StockRepository(ApplicationDBContext dbContext)
    { 
        _dbContext = dbContext;
    }
    public async Task<List<Stock>> GetAllAsync(QueryObject query)
    {
        var stocks = _dbContext.Stocks.Include(s=>s.Comments).AsQueryable();
        if (!string.IsNullOrWhiteSpace(query.CompanyName))
        {
            stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
        }

        if (!string.IsNullOrWhiteSpace(query.Symbol))
        {
            stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));
        }

        if (!string.IsNullOrWhiteSpace(query.SortBy))
        {
            if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
            {
                stocks = query.IsDescending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
            }
        }
        
        var skipNumber = (query.PageNumber - 1) * query.PageSize;
        
        return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
    }

    public async Task<Stock?> GetByIdAsync(int id)
    {
        return await _dbContext.Stocks.Include(s => s.Comments).FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Stock> CreateAsync(Stock stockModel)
    {
        await _dbContext.Stocks.AddAsync(stockModel);
        await _dbContext.SaveChangesAsync();
        return stockModel;
    }

    public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto updatedStockModel)
    {
       var stock = await _dbContext.Stocks.FindAsync(id);

       if (stock == null)
       {
           return null;
       }
       
       stock.Symbol = updatedStockModel.Symbol;
       stock.CompanyName = updatedStockModel.CompanyName;
       stock.Price = updatedStockModel.Price;
       stock.LastDividend = updatedStockModel.LastDividend;
       stock.Industry = updatedStockModel.Industry;
       stock.MarketCap = updatedStockModel.MarketCap;
       
       await _dbContext.SaveChangesAsync();
       return stock;
    }

    public async Task<Stock?> UpdateCompanyAsync(int id, UpdateCompanyNameDto updatedCompanyNameDto)
    {
        var stock = await _dbContext.Stocks.FindAsync(id);

        if (stock == null)
        {
            return null;
        }
        stock.CompanyName = updatedCompanyNameDto.CompanyName;
        await _dbContext.SaveChangesAsync();
        return stock;
    }

    public async Task<Stock?> DeleteAsync(int id)
    {
        var stockModel = await _dbContext.Stocks.FindAsync(id);
        if (stockModel != null)
        {
            _dbContext.Stocks.Remove(stockModel);
            await _dbContext.SaveChangesAsync();
        }
        return stockModel;
    }

    public Task<bool> StockExists(int stockId)
    {
        return _dbContext.Stocks.AnyAsync(s => s.Id == stockId);
    }
}