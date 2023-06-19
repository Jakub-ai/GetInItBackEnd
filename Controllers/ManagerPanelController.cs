using GetInItBackEnd.Models.Account;
using GetInItBackEnd.Services.AccountServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GetInItBackEnd.Controllers;
[Route("api/account/manager")]
[ApiController]
[Authorize(Policy = "ManagerRole")]
public class ManagerPanelController : ControllerBase
{
    private readonly IAccountService _accountService;

    public ManagerPanelController(IAccountService accountService)
    {
        _accountService = accountService;
    }
    [HttpGet("GetAllCompanyAccounts")]
    public async Task<OkObjectResult> GetAll()
    {
        var accountDtos = await _accountService.GetAllAccount();
        return Ok(accountDtos);
    }
    [HttpPost("RegisterEmployee")]
    public async Task<ActionResult> CreateAccount( [FromBody] CreateEmployeeDto dto)
    {
        var id = await _accountService.RegisterEmployee(dto);
        return Created($"/api/company/EmployeeAccount/{id}", null);
    }

    [HttpDelete("DeleteEmployee/{id}")]
    public async Task<ActionResult> DeleteAccount([FromRoute] int id)
    {
        await _accountService.DeleteAccount(id);
        return NoContent();
    }
}