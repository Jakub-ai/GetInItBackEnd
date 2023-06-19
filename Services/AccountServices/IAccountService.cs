using GetInItBackEnd.Models;
using GetInItBackEnd.Models.Account;
using GetInItBackEnd.Models.Company;

namespace GetInItBackEnd.Services.AccountServices;

public interface IAccountService
{
    public Task<AccountDto> GetAccountById(int id);
    
    public Task<IEnumerable<AccountDto>> GetAllAccount();
    public Task<int> RegisterCompanyAccount(CreateAccountDto accountDto);
    public Task<int> RegisterUser(RegisterUserDto userDto);
    public Task<string> GenerateJwt(LoginDto dto);
    public Task<int> RegisterEmployee(CreateEmployeeDto accountDto);
    public Task<ProfileDto> GetAccountProfile();
    public Task ChangeEmail(UpdateEmailDto dto);
    public Task ChangePassword(UpdatePasswordDto passwordDto);
    public Task DeleteAccount();
    public Task DeleteCompany();

}