using GetInItBackEnd.Models.Account;

namespace GetInItBackEnd.Services.AccountServices;

public interface IEmployeeAccountService
{
    public  Task<int> CreateAccount(int companyId, CreateAccountDto accountDto);
    public Task<AccountDto> GetAccountById(int id);
    public Task<IEnumerable<AccountDto>> GetAllAccount();
}