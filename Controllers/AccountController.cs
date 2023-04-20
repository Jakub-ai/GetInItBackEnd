using GetInItBackEnd.Models;
using GetInItBackEnd.Models.Account;
using GetInItBackEnd.Models.Company;
using GetInItBackEnd.Services.AccountServices;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
    
    [HttpPost("RegisterAccountCompany")]
    public async Task<ActionResult> RegisterCompanyAccount([FromBody] CreateAccountDto accountDto)
    {
        var id = await _accountService.RegisterAccount(accountDto, null);
       
        return Created($"/api/account/{id}", null);
    }
    [HttpGet("GetAllAccounts")]
    [SwaggerOperation(Summary = "Pobiera element o określonym ID", Description = "Opis szczegółowy metody pobierającej element o określonym ID.")]
    public async Task<OkObjectResult> GetAll()
    {
        var accountDtos = await _accountService.GetAllAccount();
        return Ok(accountDtos);
    }
    

 


}