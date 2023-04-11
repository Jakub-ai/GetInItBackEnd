using GetInItBackEnd.Models.Account;
using GetInItBackEnd.Models.Company;
using GetInItBackEnd.Services.CompanyServices;
using Microsoft.AspNetCore.Mvc;

namespace GetInItBackEnd.Controllers;
[Route("api/company")]
[ApiController]
public class CompanyController : ControllerBase
{
    private readonly ICompanyService _companyService;

    public CompanyController(ICompanyService companyService)
    {
        _companyService = companyService;
    }

    [HttpPost]
    public async Task<ActionResult> CreateAccount([FromRoute] int companyId,[FromBody] CreateAccountDto dto)
    {
        var id = await _companyService.CreateAccount(companyId, dto);
        return Created($"/api/company/account/{id}", null);
    }

    [HttpGet]
    public async Task<List<CompanyDto>> GetAll()
    {
        return await _companyService.GetAllCompanies();

    }
}