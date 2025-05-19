using finshark_api.Data;
using finshark_api.DTOs.Stock;
using finshark_api.Interfaces;
using finshark_api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace finshark_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StockController : ControllerBase
{
    private readonly IStockRepository _stockRepository;
    public StockController(IStockRepository stockRepository)
    {
        _stockRepository = stockRepository;
    }
    // on récupère toutes les données 
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var stocks = await _stockRepository.GetAllAsync();
        var stocksDto = stocks.Select(stock => stock.ToStockDto()); // select() = map() en JS
        return Ok(stocksDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var stock = await _stockRepository.GetByIdAsync(id);
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
        await _stockRepository.CreateAsync(stock);
        return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock.ToStockDto()); 
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updatedStockModel)
    {
        var stock = await _stockRepository.UpdateAsync(id, updatedStockModel);
        if (stock == null)
        {
            return NotFound();
        }
        return Ok(stock.ToStockDto());
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateCompanyName([FromRoute] int id, [FromBody] UpdateCompanyNameDto updatedCompanyModel)
    {
        var stock = await _stockRepository.UpdateCompanyAsync(id, updatedCompanyModel);
        if (stock == null)
        {
            return NotFound();
        }
        return Ok(stock.ToStockDto());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var stock = await _stockRepository.DeleteAsync(id);
        if (stock == null)
        {
            return NotFound();
        }
        return NoContent();
    }
}