using GetInItBackEnd.Exceptions;
using GetInItBackEnd.Models.JobApplicationDto;
using GetInItBackEnd.Services.ApplicationServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GetInItBackEnd.Controllers;
/// <summary>
/// mozna tu wpisac opis controllera i kazdego endpointu zeby zrobic dokumentacje kodu jezeli bedzie potrzebna
/// </summary>
[Route("api/JobApplications")]
[ApiController]
[Authorize(Policy = "EmployeeRole")]
[Authorize(Policy = "ManagerRole")]
[Authorize(Policy = "UserRole")]
public class JobApplicationController : ControllerBase
{
    private readonly IApplicationService _applicationService;

    public JobApplicationController(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }
    [HttpGet("SearchApplications")]
    public async Task<IActionResult> SearchApplications([FromBody] SearchApplicationDto searchDto)
    {
        try
        {
            var applications = await _applicationService.SearchApplications(searchDto);
            return Ok(applications);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpPost("CreateApplication/{offerId}")]
    public async Task<IActionResult> CreateApplication([FromForm] CreateJobApplicationDto dto,[FromRoute] int offerId,[FromForm] IFormFile file )
    {
        try
        {
            var applicationId = await _applicationService.CreateApplication(dto, offerId, file);
            return Ok(new { ApplicationId = applicationId });
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("GetAllApplications")]
    public async Task<IEnumerable<JobApplicationDto>> GetAllAppliactions()
    {
        return await _applicationService.GetAllApplications();
    }




}