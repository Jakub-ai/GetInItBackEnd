using GetInItBackEnd.Entities;

namespace GetInItBackEnd.Models.JobApplicationDto;

public class JobApplicationDto
{
    public string ApplicantName { get; set; }
    public string CompanyName { get; set; }
    public string OfferName { get; set; }
    public Level Level { get; set; }
    public string LastName { get; set; }
    public string? Email { get; set; }
    public string? ResumePath { get; set; }
    public string? Message { get; set; }
    public string? UrlLink { get; set; }
}