using GetInItBackEnd.Entities;
using GetInItBackEnd.Exceptions;
using GetInItBackEnd.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace GetInItBackEnd.Services;

public class AccountService : IAccountService
{
    private readonly GetInItDbContext _dbContext;

    public AccountService(GetInItDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AccountDto> GetAccountById(int id)
    {
        var account = await _dbContext.Accounts.FindAsync(id);

        return AccountDto.FromAccount(account ?? throw new NotFoundException("XD"));
    }

    public async Task<List<AccountDto>> GetAllAccount()
    {
        var accounts = await _dbContext.Accounts
            .Select(a => new AccountDto
            {
                Id = a.Id,
                Name = a.Name,
                LastName = a.LastName,
                Email = a.Email,
                Role = a.Role
            })
            .ToListAsync();
        
        return accounts;
    }

    public async Task<int> Create(CreateAccountDto accountDto)
    {
        if (!_dbContext.Accounts.Any()) throw new NotFoundException("not found account");

        /*var accountEntity = new Account
        {
            Name = accountDto.Name,
            LastName = accountDto.LastName,
            Email = accountDto.Email,
            Password = accountDto.Password,
            Company = new Company
            {
                Name = accountDto.Company.Name,
                Url = accountDto.Company.Url,
                Nip = accountDto.Company.Nip,
                Regon = accountDto.Company.Regon,
            }
        };*/
        var accountEntity = accountDto.ToAccount();
        
        
        
        await _dbContext.Accounts.AddAsync(accountEntity);
        await _dbContext.SaveChangesAsync();
         return accountEntity.Id;
    }
}