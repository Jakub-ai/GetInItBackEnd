using System.ComponentModel.DataAnnotations;
using GetInItBackEnd.Entities;

namespace GetInItBackEnd.Models;

public class CreateAccountDto
{
    [Required][MaxLength(25)]
    public string Name { get; set; }
    [Required][MaxLength(25)]
    public string LastName { get; set; }
    [Required][MaxLength(25)]
    public string Email { get; set; }
    [Required]
    public Role Role { get; set; }
}