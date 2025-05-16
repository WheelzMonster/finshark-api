namespace finshark_api.DTOs.Stock;


//le DTO de stock par exemple ici, ne contient pas la propriété Comment car on ne veut pas la rendre accessible à l'utilisateur, on modifie la structure de l'objet, et ses données avec un mapper
public class StockDto
{
    public int Id { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty; 
    public decimal Price { get; set; }
    public decimal LastDividend { get; set; }
    public string Industry { get; set; } = string.Empty;
    public long MarketCap { get; set; }
}