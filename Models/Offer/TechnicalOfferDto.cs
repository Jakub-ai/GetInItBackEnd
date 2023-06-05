using GetInItBackEnd.Entities;

namespace GetInItBackEnd.Models.Offer;

public class TechnicalOfferDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string PrimarySkill { get; set; }
    public int CompanyId { get; set; }
    public string CompanyName { get; set; }
    public decimal? SalaryFrom { get; set; }
    public decimal? SalaryTo { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public List<TechnologyDto> Technologies { get; set; }
    public string City { get; set; }
    public Level Level { get; set; }
    public WorkingPlace Place { get; set; }
}