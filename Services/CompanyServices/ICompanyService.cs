using GetInItBackEnd.Entities;
using GetInItBackEnd.Models.Account;
using GetInItBackEnd.Models.Company;

namespace GetInItBackEnd.Services.CompanyServices;

public interface ICompanyService
{
 
    Task<List<CompanyDto>> GetAllCompanies();
    public  Task Update (int id, UpdateCompanyDto dto);

}