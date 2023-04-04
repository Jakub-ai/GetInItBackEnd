using GetInItBackEnd.Entities;

namespace GetInItBackEnd.Models;

public class CompanyDto
{
    public string Name { get; set; }
    public string? Url { get; set; }

    public static CompanyDto FromCompanyDto(Company company)
    {
        return new CompanyDto
        {
            Name = company.Name,
            Url = company.Url
        };
    }
}