namespace GetInItBackEnd.Models.JobApplicationDto;

public class CreateJobApplicationDto
{
    public string? ApplicantName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? ResumePath { get; set; }
    public string? Message { get; set; }
    public string? UrlLink { get; set; }

    public int OfferId { get; set; }
    public int? CreatedById { get; set; }
}