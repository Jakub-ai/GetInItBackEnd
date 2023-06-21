using GetInItBackEnd.Entities;

namespace GetInItBackEnd.Models;

public class CreateOfferDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? PrimarySkill { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? City { get; set; }
    public decimal? SalaryFrom { get; set; }
    public decimal? SalaryTo { get; set; }
    public Level? Level { get; set; }
    public WorkingPlace? Place { get; set; }
    public List<TechnologyDto>? Technologies { get; set; }
}