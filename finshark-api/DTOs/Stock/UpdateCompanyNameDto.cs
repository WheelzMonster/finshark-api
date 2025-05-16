using System.ComponentModel.DataAnnotations;

namespace finshark_api.DTOs.Stock;

public class UpdateCompanyNameDto
{
    [Required]
    public string CompanyName { get; set; } = string.Empty;
}