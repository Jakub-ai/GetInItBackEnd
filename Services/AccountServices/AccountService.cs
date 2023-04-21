using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using GetInItBackEnd.Authentication;
using GetInItBackEnd.Entities;
using GetInItBackEnd.Exceptions;
using GetInItBackEnd.Middleware;
using GetInItBackEnd.Models;
using GetInItBackEnd.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace GetInItBackEnd.Services.AccountServices;

public class AccountService : IAccountService
{
    private readonly GetInItDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILogger<AccountService> _logger;
    private readonly IPasswordHasher<Account> _passwordHasher;
    private readonly AuthenticationSettings _authenticationSettings;

    public AccountService(GetInItDbContext dbContext, IMapper mapper, ILogger<AccountService> logger,IPasswordHasher<Account> passwordHasher, AuthenticationSettings authenticationSettings)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _logger = logger;
        _passwordHasher = passwordHasher;
        _authenticationSettings = authenticationSettings;
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
            .ThenInclude(a => a!.Address)
            .ToListAsync();
        var accountsDto = _mapper.Map<List<AccountDto>>(accounts);
        return accountsDto;
    }
    
    

    public async Task<int> RegisterAccount(CreateAccountDto accountDto, int? companyId )
    {
        var account = _mapper.Map<Account>(accountDto);
        account.CompanyId = companyId;
        //account.Password = accountDto.Password;
        if (accountDto.CreateCompanyDto is not null)
        {
            account.Company = _mapper.Map<Company>(accountDto.CreateCompanyDto);
        }
        var hashedPassword = _passwordHasher.HashPassword(account, accountDto.Password);

        account.PasswordHash = hashedPassword;
        await _dbContext.Accounts.AddAsync(account);
       // await _dbContext.Companies.AddAsync(company);
        await _dbContext.SaveChangesAsync();
        return account.Id;
    }
    public async Task<string> GenerateJwt(LoginDto dto)
    {
        var account = await _dbContext.Accounts
            .FirstOrDefaultAsync(u => u.Email == dto.Email);
        if (account is null)
        {
            throw new BadRequestException("Invalid email or password");
            
        }
        var result =   _passwordHasher.VerifyHashedPassword(account, account.PasswordHash, dto.Password);
        if (result == PasswordVerificationResult.Failed )
        {
            throw new BadRequestException("Invalid email or password");
        }

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
            new Claim(ClaimTypes.Name, $"{account.Name}"),
            new Claim(ClaimTypes.Role, $"{account.Role}")
           
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

        var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer, _authenticationSettings.JwtIssuer, claims,
            expires: expires,
            signingCredentials: cred);

        var tokenHandler = new JwtSecurityTokenHandler();

        return tokenHandler.WriteToken(token);
    }

  
}