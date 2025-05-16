using finshark_api.Data;
using finshark_api.DTOs.Stock;
using finshark_api.Mappers;
using Microsoft.AspNetCore.Mvc;

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
    public IActionResult GetAll()
    {
        var stocks = _dbContext.Stocks.Select(s => s.ToStockDto()).ToList(); // Select = map() en JS
        return Ok(stocks);
    }

    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute] int id)
    {
        var stock = _dbContext.Stocks.Find(id);
        if (stock == null)
        {
            return NotFound();
        }
        return Ok(stock.ToStockDto());
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateStockRequestDto createdStockModel)
    {
        var stock = createdStockModel.ToStockFromCreatedDto();
        _dbContext.Stocks.Add(stock);
        _dbContext.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock.ToStockDto()); 
    }

    [HttpPut("{id}")]
    public IActionResult Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updatedStockModel)
    {
        var stock = _dbContext.Stocks.Find(id);
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

        _dbContext.SaveChanges();
        return Ok(stock.ToStockDto());
    }

    [HttpPatch("{id}")]
    public IActionResult UpdateCompanyName([FromRoute] int id, [FromBody] UpdateCompanyNameDto updatedCompanyModel)
    {
        var stock = _dbContext.Stocks.Find(id);
        if (stock == null)
        {
            return NotFound();
        }
        stock.CompanyName = updatedCompanyModel.CompanyName;
        
        _dbContext.SaveChanges();
        return Ok(stock.ToStockDto());
    }

    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var stock = _dbContext.Stocks.Find(id);
        if (stock == null)
        {
            return NotFound();
        }
        _dbContext.Stocks.Remove(stock);
        _dbContext.SaveChanges();
        return NoContent();
    }
}