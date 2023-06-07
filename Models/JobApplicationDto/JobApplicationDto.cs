namespace GetInItBackEnd.Models.JobApplicationDto;

public class JobApplicationDto
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string? Email { get; set; }
    public byte[] Resume { get; set; }
    public string? Message { get; set; }
    public string? UrlLink { get; set; }
}