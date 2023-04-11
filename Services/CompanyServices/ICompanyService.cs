using GetInItBackEnd.Entities;
using GetInItBackEnd.Models.Account;
using GetInItBackEnd.Models.Company;

namespace GetInItBackEnd.Services.CompanyServices;

public interface ICompanyService
{
    Task<int> CreateAccount(int companyId, CreateAccountDto accountDto);
    Task<List<CompanyDto>> GetAllCompanies();

}