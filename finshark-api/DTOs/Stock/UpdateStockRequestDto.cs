using System.ComponentModel.DataAnnotations;

namespace finshark_api.DTOs.Stock;

// null! signifie qu'on promet au compilateur que la variable sera remplie avant usage
public class UpdateStockRequestDto
{
    [Required]
    [MinLength(1)]
    public string Symbol { get; set; } = null!;

    [Required]
    [MinLength(1)]
    public string CompanyName { get; set; } = null!;

    [Required]
    [MinLength(1)]
    public string Industry { get; set; } = null!;
    
    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public decimal LastDividend { get; set; }

    [Required]
    [Range(1, long.MaxValue)]
    public long MarketCap { get; set; }
}
