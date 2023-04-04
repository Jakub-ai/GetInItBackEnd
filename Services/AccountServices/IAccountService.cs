using GetInItBackEnd.Models;

namespace GetInItBackEnd.Services.AccountServices;

public interface IAccountService
{
    public Task<AccountDto> GetAccountById(int id);
    public Task<int> Create(CreateAccountDto accountDto);
    public Task<IEnumerable<AccountDto>> GetAllAccount();
    public Task<string> getString();
}