namespace GetInItBackEnd.Models.JobApplicationDto;

public class SearchApplicationDto
{
    public int? Id { get; set; }
    public string? ApplicantName { get; set; }
    public string? OfferName { get; set; }
    public string? LastName { get; set; }
    public int? OfferId { get; set; }
}