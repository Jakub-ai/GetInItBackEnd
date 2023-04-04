using GetInItBackEnd.Models;
using GetInItBackEnd.Services.AccountServices;
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

    [HttpPost]
    public async Task<ActionResult> CreateAccount([FromBody] CreateAccountDto dto)
    {
        var id = await _accountService.Create(dto);
        return Created($"/api/account/{id}", null);
    }

    [HttpGet]
    public async Task<OkObjectResult> GetAll()
    {
        var accountDtos = await _accountService.GetAllAccount();
        return Ok(accountDtos);
    }
    

}