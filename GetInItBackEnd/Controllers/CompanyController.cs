using GetInItBackEnd.Models.Account;
using GetInItBackEnd.Models.Company;
using GetInItBackEnd.Services.CompanyServices;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GetInItBackEnd.Controllers;
[Route("api/account/company")]
[ApiController]
public class CompanyController : ControllerBase
{
    private readonly ICompanyService _companyService;

    public CompanyController(ICompanyService companyService)
    {
        _companyService = companyService;
    }



    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Pobiera element o określonym ID", Description = "Opis szczegółowy metody pobierającej element o określonym ID.")]
    public async Task<ActionResult> Update([FromBody] UpdateCompanyDto dto, [FromRoute] int id)
    {
       await _companyService.Update(id, dto);
        
        return Ok() ;
    }

    [HttpGet("GetAllCompanies")]
    public async Task<List<CompanyDto>> GetAll()
    {
        return await _companyService.GetAllCompanies();

    }
}