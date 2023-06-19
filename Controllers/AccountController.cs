using GetInItBackEnd.Models.Account;
using GetInItBackEnd.Services.AccountServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GetInItBackEnd.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }
    
    [HttpPost("RegisterCompanyAccount")]
    public async Task<ActionResult> RegisterCompanyAccount([FromBody] CreateAccountDto accountDto)
    {
        var id = await _accountService.RegisterCompanyAccount(accountDto);
       
        return Created($"/api/account/{id}", null);
    }
    [HttpPost("RegisterUserAccount")]
    public async Task<ActionResult> RegisterUserAccount([FromBody] RegisterUserDto userDto)
    {
        var id = await _accountService.RegisterUser(userDto);
       
        return Created($"/api/account/{id}", null);
    }
   
    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginDto dto)
    {
        var token = await _accountService.GenerateJwt(dto);
        return Ok(token);
    }
    [Authorize(Policy = "EmployeeRole")]
    [Authorize(Policy = "UserRole")]
    [HttpDelete("DeleteAccount")]
    public async Task<ActionResult> DeleteAccount()
    {
        await _accountService.DeleteAccount(null);
        return NoContent();
    }
   
    [Authorize(Policy = "ManagerRole")]
    [HttpDelete("DeleteAccountAndCompany")]
    public async Task<ActionResult> DeleteOffer()
    {
        //await _accountService.DeleteAccount();
        await _accountService.DeleteCompany();
        return NoContent();
    }

    [HttpGet("AccountProfile")]
    public async Task<ActionResult> GetProfileInfo()
    {
        var accountInfo = await _accountService.GetAccountProfile();
        return Ok(accountInfo);
    }

    [HttpPut("ChangeEmail")]
    public async Task<ActionResult> ChangeEmail([FromBody] UpdateEmailDto dto)
    {
        await _accountService.ChangeEmail(dto);
        return Ok();
    }

    [HttpPut("ChangePassword")]
    public async Task<ActionResult> ChangePassword([FromBody] UpdatePasswordDto dto)
    {
        await _accountService.ChangePassword(dto);
        return Ok();
    }

}