using GetInItBackEnd.Models.Account;
using GetInItBackEnd.Services.AccountServices;
using Microsoft.AspNetCore.Mvc;

namespace GetInItBackEnd.Controllers;
[Route("api/company/{companyId}/EmployeeAccount")]
[ApiController]
public class EmployeeAccountController : ControllerBase
{
    private readonly IAccountService _service;

    public EmployeeAccountController(IAccountService service)
    {
        _service = service;
    }

    [HttpPost("RegisterEmployee")]
    public async Task<ActionResult> CreateAccount( int companyId,[FromBody] CreateAccountDto dto)
    {
        var id = await _service.RegisterAccount(dto, companyId);
        return Created($"/api/company/{companyId}/EmployeeAccount/{id}", null);
    }

    
}