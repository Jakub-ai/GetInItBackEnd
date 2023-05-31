using System.ComponentModel.DataAnnotations.Schema;

namespace GetInItBackEnd.Entities;

public class Offer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string PrimarySkill { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string City { get; set; }
    public decimal? SalaryFrom { get; set; }
    public decimal? SalaryTo { get; set; }
    public Level Level { get; set; }
    public WorkingPlace Place { get; set; }
    public int CompanyId { get; set; }
    public int? CreatedById { get; set; }
    public virtual Account? CreatedBy { get; set; }

    public virtual Company Company { get; set; }
    public virtual List<Technology> Technologies { get; set; }
    public virtual List<EmploymentType> EmploymentTypes { get; set; }
    public virtual List<JobApplication>? JobApplications { get; set; }
    
    
}