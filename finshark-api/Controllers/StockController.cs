using finshark_api.Data;
using finshark_api.DTOs.Stock;
using finshark_api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace finshark_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StockController : ControllerBase
{
    private readonly ApplicationDBContext _dbContext;
    public StockController(ApplicationDBContext dbContext)
    {
        _dbContext = dbContext;
    }
    // on récupère toutes les données 
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var stocks = await _dbContext.Stocks.Select(s => s.ToStockDto()).ToListAsync(); // Select = map() en JS
        return Ok(stocks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var stock = await _dbContext.Stocks.FindAsync(id);
        if (stock == null)
        {
            return NotFound();
        }
        return Ok(stock.ToStockDto());
    }

    [HttpPost]
    public async Task <IActionResult> Create([FromBody] CreateStockRequestDto createdStockModel)
    {
        var stock = createdStockModel.ToStockFromCreatedDto();
        await _dbContext.Stocks.AddAsync(stock);
        await _dbContext.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock.ToStockDto()); 
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updatedStockModel)
    {
        var stock = await _dbContext.Stocks.FindAsync(id);
        if (stock == null)
        {
            return NotFound();
        }
        
        stock.Symbol = updatedStockModel.Symbol;
        stock.CompanyName = updatedStockModel.CompanyName;
        stock.Price = updatedStockModel.Price;
        stock.LastDividend = updatedStockModel.LastDividend;
        stock.Industry = updatedStockModel.Industry;
        stock.MarketCap = updatedStockModel.MarketCap;

        await _dbContext.SaveChangesAsync();
        return Ok(stock.ToStockDto());
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateCompanyName([FromRoute] int id, [FromBody] UpdateCompanyNameDto updatedCompanyModel)
    {
        var stock = await _dbContext.Stocks.FindAsync(id);
        if (stock == null)
        {
            return NotFound();
        }
        stock.CompanyName = updatedCompanyModel.CompanyName;
        
       await _dbContext.SaveChangesAsync();
        return Ok(stock.ToStockDto());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var stock = await _dbContext.Stocks.FindAsync(id);
        if (stock == null)
        {
            return NotFound();
        }
        _dbContext.Stocks.Remove(stock);
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }
}