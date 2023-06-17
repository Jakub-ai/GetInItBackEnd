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

public class JobApplicationController : ControllerBase
{
    private readonly IApplicationService _applicationService;

    public JobApplicationController(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }
    [Authorize(Policy = "EmployeeRole")]
    [Authorize(Policy = "ManagerRole")]
    [Authorize(Policy = "UserRole")]
    [HttpPost("SearchApplications")]
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
    
    /// <summary>
    /// controller for creating new jobapplications by user
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="offerId"></param>
    /// <param name="file"></param>
    /// <returns></returns>
    [Authorize(Policy = "UserRole")]
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
    [Authorize(Policy = "EmployeeRole")]
    [Authorize(Policy = "ManagerRole")]
    [Authorize(Policy = "UserRole")]
    [HttpGet("GetAllApplications")]
    public async Task<IEnumerable<JobApplicationDto>> GetAllAppliactions()
    {
        return await _applicationService.GetAllApplications();
    }
    [Authorize(Policy = "EmployeeRole")]
    [Authorize(Policy = "ManagerRole")]
    [HttpGet("DownloadFile/{offerId}/{userId}/{fileName}")]
    public async Task<IActionResult> DownloadResumeFile([FromRoute]string offerId, [FromRoute]string userId,[FromRoute] string fileName)
    {
        try
        {
            var fileData = await _applicationService.GetResumeFile(offerId, userId, fileName);
            return File(fileData.Item1, fileData.Item2, fileData.Item3);
        }
        catch (FileNotFoundException)
        {
            return NotFound("File not found");
        }
        catch (Exception ex)
        {
            // handle other exceptions as necessary
            return StatusCode(500, $"Internal server error: {ex}");
        }
    }




}