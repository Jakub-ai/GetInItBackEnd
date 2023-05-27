using GetInItBackEnd.Models;
using GetInItBackEnd.Models.Account;
using GetInItBackEnd.Models.Company;

namespace GetInItBackEnd.Services.AccountServices;

public interface IAccountService
{
    public Task<AccountDto> GetAccountById(int id);
    
    public Task<IEnumerable<AccountDto>> GetAllAccount();
    public Task<int> RegisterAccount(CreateAccountDto accountDto, int? companyId);
    public Task<string> GenerateJwt(LoginDto dto);
    public Task<int> RegisterEmployee(CreateEmployeeDto accountDto);

}