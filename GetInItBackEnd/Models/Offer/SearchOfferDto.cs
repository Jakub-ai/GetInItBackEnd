using GetInItBackEnd.Entities;

namespace GetInItBackEnd.Models.Offer;

public class SearchOfferDto
{
    public string? Name { get; set; }
    public string? PrimarySkill { get; set; }
    public string? CompanyName { get; set; }
    public List<TechnologyDto>? Technologies { get; set; }
    public string? City { get; set; }
    public Level? Level { get; set; }
    public WorkingPlace? Place { get; set; }
}