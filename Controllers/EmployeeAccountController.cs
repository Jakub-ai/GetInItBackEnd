using GetInItBackEnd.Models.Account;
using GetInItBackEnd.Services.AccountServices;
using Microsoft.AspNetCore.Mvc;

namespace GetInItBackEnd.Controllers;
[Route("api/company/{companyId}/EmployeeAccount")]
[ApiController]
public class EmployeeAccountController : ControllerBase
{
    private readonly IEmployeeAccountService _service;

    public EmployeeAccountController(IEmployeeAccountService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult> CreateAccount([FromRoute] int companyId,[FromBody] CreateAccountDto dto)
    {
        var id = await _service.CreateAccount(companyId, dto);
        return Created($"/api/company/{companyId}/EmployeeAccount/{id}", null);
    }

    /*public Task<AccountDto> GetAccountById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<AccountDto>> GetAllAccount()
    {
        throw new NotImplementedException();
    }*/

 
}