using GetInItBackEnd.Models;
using GetInItBackEnd.Models.Account;

namespace GetInItBackEnd.Services.AccountServices;

public interface IAccountService
{
    public Task<AccountDto> GetAccountById(int id);
   
    public Task<int> CreateCompanyAccount(CreateAccountCompanyDto dto);
    public Task<IEnumerable<AccountDto>> GetAllAccount();
    
   
}