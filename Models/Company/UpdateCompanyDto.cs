using System.Diagnostics.CodeAnalysis;

namespace GetInItBackEnd.Models.Company;

public class UpdateCompanyDto
{
 
    public string? Url { get; set; }
    public string? Industry { get; set; }
    public string? Description { get; set; }
}