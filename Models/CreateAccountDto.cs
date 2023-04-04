﻿using System.ComponentModel.DataAnnotations;
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
    public string Password { get; set; }

    public CreateCompanyDto Company{ get; set; }


    public Account ToAccount()
    {
        return new Account
        {
            Name = Name,
            LastName = LastName,
            Email = Email,
            Password = Password,
            Company = new Company
            {
                Name = Company.Name,
                Url = Company.Url,
                Nip = Company.Nip,
                Regon = Company.Regon,
            }

        };
    }
    
}