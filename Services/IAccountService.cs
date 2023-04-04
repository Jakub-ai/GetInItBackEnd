using GetInItBackEnd.Models;

namespace GetInItBackEnd.Services;

public interface IAccountService
{
    public Task<AccountDto> GetAccountById(int id);
    public Task<int> Create(CreateAccountDto accountDto);
    public Task<List<AccountDto>> GetAllAccount();
}