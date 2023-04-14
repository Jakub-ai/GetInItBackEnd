using AutoMapper;
using GetInItBackEnd.Entities;
using GetInItBackEnd.Exceptions;
using GetInItBackEnd.Models;
using GetInItBackEnd.Models.Account;
using Microsoft.EntityFrameworkCore;

namespace GetInItBackEnd.Services.AccountServices;

public class AccountService : IAccountService
{
    private readonly GetInItDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILogger<AccountService> _logger;

    public AccountService(GetInItDbContext dbContext, IMapper mapper, ILogger<AccountService> logger)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<AccountDto> GetAccountById(int id)
    {
        var account = await _dbContext.Accounts
            .Include(a => a.Company)
            .FirstOrDefaultAsync(a => a.Id == id);
        if (account is null) throw new NotFoundException("Account not found");

        var result = _mapper.Map<AccountDto>(account);
        return result;

    }

    public async Task<IEnumerable<AccountDto>> GetAllAccount()
    {
        var accounts = await _dbContext.Accounts
            .Include(a => a.Company)
            .ToListAsync();
        var accountsDto = _mapper.Map<List<AccountDto>>(accounts);
        return accountsDto;
    }

    public async Task<int> CreateCompanyAccount(CreateAccountCompanyDto dto)
    {
        var account = _mapper.Map<Account>(dto);
        await _dbContext.Accounts.AddAsync(account);
        await _dbContext.SaveChangesAsync();
        return account.Id;
    }
    public async Task<int> CreateAccount(CreateAccountDto accountDto)
    {
        //if (!_dbContext.Accounts.Any()) throw new NotFoundException("not found account");

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
        var account = _mapper.Map<Account>(accountDto);
        await _dbContext.Accounts.AddAsync(account);
        await _dbContext.SaveChangesAsync();
        return account.Id;


    }
}