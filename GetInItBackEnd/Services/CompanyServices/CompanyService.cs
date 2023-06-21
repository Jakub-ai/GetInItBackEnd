using AutoMapper;
using GetInItBackEnd.Entities;
using GetInItBackEnd.Exceptions;
using GetInItBackEnd.Models.Account;
using GetInItBackEnd.Models.Company;
using Microsoft.EntityFrameworkCore;

namespace GetInItBackEnd.Services.CompanyServices;

public class CompanyService : ICompanyService
{
    private readonly GetInItDbContext _dbContext;
    private readonly IMapper _mapper;

    public CompanyService(GetInItDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }



    public async Task<List<CompanyDto>> GetAllCompanies()
    {
        var companies = await _dbContext.Companies
            .Include(z => z.Accounts)
            .Include(z => z.Address)
            .ToListAsync();
        var companyDto = _mapper.Map<List<CompanyDto>>(companies);
        return companyDto;
    }

    public async Task Update(int id, UpdateCompanyDto dto)
    {
        var company = await GetCompanyIdAsync(id);

        company.Url = dto.Url ?? company.Url;
        company.Description = dto.Description ?? company.Description;
        company.Industry = dto.Industry ?? company.Industry;

         await _dbContext.SaveChangesAsync();
    }

    private async Task<Company?> GetCompanyIdAsync(int companyId)
    {
        var company = await _dbContext.Companies.FirstOrDefaultAsync(c => c.Id == companyId);
        if (company is null) throw new NotFoundException("Company Not Found");

        return company;

    }
    
}