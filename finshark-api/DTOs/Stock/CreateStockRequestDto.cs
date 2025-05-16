namespace finshark_api.DTOs.Stock;

public class CreateStockRequestDto
{
    public string Symbol { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty; 
    public decimal Price { get; set; }
    public decimal LastDividend { get; set; }
    public string Industry { get; set; } = string.Empty;
    public long MarketCap { get; set; }
}