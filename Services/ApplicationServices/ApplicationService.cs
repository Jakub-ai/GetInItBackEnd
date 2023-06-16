using AutoMapper;
using GetInItBackEnd.Entities;
using GetInItBackEnd.Exceptions;
using GetInItBackEnd.Models.JobApplicationDto;
using GetInItBackEnd.Services.AccountServices;
using Microsoft.EntityFrameworkCore;

namespace GetInItBackEnd.Services.ApplicationServices;

public class ApplicationService : IApplicationService
{
    private readonly GetInItDbContext _dbContext;
    private readonly IUserContextService _userContextService;
    private readonly IMapper _mapper;

    public ApplicationService(GetInItDbContext dbContext, IUserContextService userContextService, IMapper mapper)
    {
        _dbContext = dbContext;
        _userContextService = userContextService;
        _mapper = mapper;
    }

    public async Task<int> CreateApplication(CreateJobApplicationDto dto, int offerId, IFormFile file)
    {
        
        var offer = await _dbContext.Offers.FirstOrDefaultAsync(o => o.Id == offerId);
        if (offer is null) throw new NotFoundException("offer does not exist");
        var userId = _userContextService.GetUserId;
        dto.ApplicantName = _userContextService.GetUserName;
        dto.LastName = _userContextService.GetUserLastName;
        dto.Email = _userContextService.GetUserMail;
        dto.CreatedById = userId;
        dto.OfferId = offerId;

        if (file is { Length: > 0 })
        {
            var rootPath = Directory.GetCurrentDirectory();
            var fileName = file.FileName;
            var folderPath = $"{rootPath}\\wwwroot\\OfferFiles\\{offerId}\\{userId}".Replace('\\', '/');
            var fullPath = Path.Combine(folderPath, fileName);
            Directory.CreateDirectory(folderPath);
            await using (var stream = new FileStream(fullPath,FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            dto.ResumePath = fullPath;
            if (dto.ResumePath is null) throw new NotFoundException("Resume must be added");

        }

        var application = _mapper.Map<JobApplication>(dto);
        
        await _dbContext.JobApplications.AddAsync(application);
        await _dbContext.SaveChangesAsync();
        return application.Id;
        }

    public async Task<IEnumerable<JobApplicationDto>> GetAllApplications()
    {
        var userId = _userContextService.GetUserId;
        var companyId = _userContextService.GetCompanyId;
        var userRole = _userContextService.GetUserRole;
        List<JobApplication> applications = new List<JobApplication>();

        if (userRole == Role.ManagerCompanyAccount.ToString())
        {
            applications = await _dbContext.JobApplications
                .Include(o => o.Offer)
                .Where(o => o.Offer.CompanyId == companyId)
                .ToListAsync();
        }
        else if (userRole == Role.EmployeeAccount.ToString())
        {
            var offersCreatedByUser = await _dbContext.Offers
                .Where(o => o.CreatedById == userId)
                .ToListAsync();

            applications = await _dbContext.JobApplications
                .Include(o => o.Offer)
                .Where(o => offersCreatedByUser.Select(offer => offer.Id).Contains(o.OfferId))
                .ToListAsync();
        }
        else if (userRole == Role.UserAccount.ToString())
        {
            applications = await _dbContext.JobApplications
                .Include(o => o.Offer)
                .Where(o => o.CreatedById == userId)
                .ToListAsync();
        }
        else
        {
            throw new UnauthorizedAccessException("not allowed");
        }

        var results = _mapper.Map<List<JobApplicationDto>>(applications);
        return results;
    }
    
    public async Task<IEnumerable<JobApplicationDto>> SearchApplications(SearchApplicationDto searchDto)
    {
        var companyId = _userContextService.GetCompanyId;
        var userId = _userContextService.GetUserId;
        var userRole = _userContextService.GetUserRole;
        IQueryable<JobApplication> applications = _dbContext.JobApplications.AsQueryable();

        // Filter applications based on the user role
        if (userRole == Role.ManagerCompanyAccount.ToString())
        {
            applications = applications.Where(a => a.Offer.CompanyId == companyId);
        }
        else if (userRole == Role.EmployeeAccount.ToString())
        {
            var offersCreatedByUser = await _dbContext.Offers.Where(o => o.CreatedById == userId).Select(o => o.Id).ToListAsync();
            applications = applications.Where(a => offersCreatedByUser.Contains(a.OfferId));
        }
        else if (userRole == Role.UserAccount.ToString())
        {
            applications = applications.Where(a => a.CreatedById == userId);
        }
        else
        {
            throw new UnauthorizedAccessException(" not allowed");
        }

        if (!string.IsNullOrEmpty(searchDto.LastName))
        {
            applications = applications.Where(a => a.LastName.ToUpper() == searchDto.LastName.ToUpper());
        }
        if (searchDto.Id is not null)
        {
            applications = applications.Where(a => a.Id == searchDto.Id);
        }
        if (!string.IsNullOrEmpty(searchDto.ApplicantName))
        {
            applications = applications.Where(a => a.Name == searchDto.ApplicantName.ToUpper());
        }
        if (!string.IsNullOrEmpty(searchDto.OfferName))
        {
            applications = applications.Where(a => a.Name == searchDto.OfferName.ToUpper());
        }
   
        var results = await applications.Include(a => a.Offer).ToListAsync();
        var applicationDtos = _mapper.Map<List<JobApplicationDto>>(results);

        return applicationDtos;
    }


}