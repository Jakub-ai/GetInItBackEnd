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



    [HttpPut("{id}")]
    public async Task<ActionResult> Update([FromBody] UpdateCompanyDto dto, [FromRoute] int id)
    {
       await _companyService.Update(id, dto);
        
        return Ok() ;
    }

    [HttpGet]
    public async Task<List<CompanyDto>> GetAll()
    {
        return await _companyService.GetAllCompanies();

    }
}