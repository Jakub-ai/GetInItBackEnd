using Microsoft.AspNetCore.Http.HttpResults;

namespace GetInItBackEnd.Entities;

public class JobApplication
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string? Email { get; set; }
    public string? ResumePath { get; set; }
    public string? UrlLink { get; set; }
    public string? Message { get; set; }
    public int OfferId { get; set; }
    public int? CreatedById { get; set; }
    public virtual Account? CreatedBy { get; set; }

    public virtual Offer Offer { get; set; }
}