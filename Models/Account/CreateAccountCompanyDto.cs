using System.ComponentModel.DataAnnotations;
using GetInItBackEnd.Entities;

namespace GetInItBackEnd.Models.Account;

public class CreateAccountCompanyDto
{
    [Required][MaxLength(25)]
    public string Name { get; set; }
    [Required][MaxLength(25)]
    public string LastName { get; set; }
    [Required][MaxLength(50)]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
  
    
    public string CompanyName { get; set; }
    public string? Url { get; set; }
    public string? Industry { get; set; }
    public long Nip { get; set; }
    public int Regon { get; set; }
    
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string BuildingNumber { get; set; }
    public string PostalCode { get; set; }
    
}