using GetInItBackEnd.Exceptions;
using GetInItBackEnd.Models.JobApplicationDto;
using GetInItBackEnd.Services.ApplicationServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GetInItBackEnd.Controllers;
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



    
}