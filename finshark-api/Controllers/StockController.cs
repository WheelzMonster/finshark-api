using finshark_api.Data;
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
}