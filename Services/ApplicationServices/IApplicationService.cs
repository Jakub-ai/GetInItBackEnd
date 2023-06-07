using GetInItBackEnd.Models.JobApplicationDto;

namespace GetInItBackEnd.Services.ApplicationServices;

public interface IApplicationService
{
    public Task<int> CreateApplication(CreateJobApplicationDto dto, int offerId);
    Task<IEnumerable<JobApplicationDto>> GetAllApplications();
    Task<IEnumerable<JobApplicationDto>> SearchApplications(SearchApplicationDto searchDto);
}