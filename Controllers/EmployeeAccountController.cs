using GetInItBackEnd.Models.Account;
using GetInItBackEnd.Services.AccountServices;
using Microsoft.AspNetCore.Mvc;

namespace GetInItBackEnd.Controllers;
[Route("api/company/EmployeeAccount")]
[ApiController]
public class EmployeeAccountController : ControllerBase
{
    private readonly IAccountService _service;

    public EmployeeAccountController(IAccountService service)
    {
        _service = service;
    }

    [HttpPost("RegisterEmployee")]
    public async Task<ActionResult> CreateAccount( [FromBody] CreateEmployeeDto dto)
    {
        var id = await _service.RegisterEmployee(dto);
        return Created($"/api/company/EmployeeAccount/{id}", null);
    }

    
}