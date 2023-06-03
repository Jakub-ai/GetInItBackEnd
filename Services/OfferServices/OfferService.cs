using AutoMapper;
using GetInItBackEnd.Entities;
using GetInItBackEnd.Exceptions;
using GetInItBackEnd.Models;
using GetInItBackEnd.Models.Offer;
using GetInItBackEnd.Services.AccountServices;
using Microsoft.EntityFrameworkCore;

namespace GetInItBackEnd.Services.OfferServices;

public class OfferService : IOfferService
{
    private readonly GetInItDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IUserContextService _userContextService;

    public OfferService(GetInItDbContext dbContext, IMapper mapper, IUserContextService userContextService)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _userContextService = userContextService;
    }

    public async Task<int> Create(CreateOfferDto dto)
    {
        var address =
            await _dbContext.Addresses.FirstOrDefaultAsync(a => a.Company.Id == (int)_userContextService.GetCompanyId);
        dto.City = address.City;
        var offer = _mapper.Map<Offer>(dto);
        offer.CreatedById = (int)_userContextService.GetUserId;
        offer.CompanyId = (int)_userContextService.GetCompanyId;
        

        await _dbContext.Offers.AddAsync(offer);
        await _dbContext.SaveChangesAsync();
        return offer.Id;
    }


    public async Task<IEnumerable<OfferDto>> GetByName(string name)
    {
        var lowerCaseName = name.ToLower();
        var offers = await _dbContext.Offers
            .Where(o => o.Name.ToLower().StartsWith(lowerCaseName))
            .Include(o => o.Company)
            .ToListAsync();

        if (!offers.Any())
            throw new NotFoundException("Offer is not found");

        var result = _mapper.Map<List<OfferDto>>(offers);
        return result;
    }

    public async Task<IEnumerable<OfferDto>> GetByPrimarySkill(string primarySkill)
    {
        var lowerCaseName = primarySkill.ToLower();
        var offers = await _dbContext.Offers
            .Where(o => o.PrimarySkill.ToLower().StartsWith(lowerCaseName))
            .Include(o => o.Company)
            .Include(o => o.Technologies)
            .ToListAsync();

        if (!offers.Any())
            throw new NotFoundException("Offer is not found");

        var result = _mapper.Map<List<OfferDto>>(offers);
        return result;
    }

    public async Task<IEnumerable<OfferDto>> GetByTechnology(string tech)
    {
        var lowerCaseTech = tech.ToLower();

        var offers = await _dbContext.Offers
            .Where(o => o.Technologies.Any(t => t.Skill.ToLower() == lowerCaseTech))
            .Include(o => o.Company)
            .Include(o => o.Technologies)
            .ToListAsync();

        if (!offers.Any())
            throw new NotFoundException("Offer is not found");

        var result = _mapper.Map<List<OfferDto>>(offers);
        return result;
    }

    public async Task<IEnumerable<OfferDto>> GetOffers()
    {
        var offers = await _dbContext.Offers
            .Include(o => o.Company)
            .Include(t => t.Technologies).ToListAsync();
        var offerDtos = _mapper.Map<List<OfferDto>>(offers);
        return offerDtos;
    }

    public async Task<IEnumerable<OfferDto>> GetAllCompanyOffers()
    {
        var companyId = _userContextService.GetCompanyId;
        var offers = await _dbContext.Offers
            .Include(o => o.Company)
            .Include(o => o.Technologies)
            .Where(o => o.CompanyId == companyId).ToListAsync();
        var offerDtos = _mapper.Map<List<OfferDto>>(offers);
        return offerDtos;
    }
    public async Task<IEnumerable<OfferDto>> GetAllEmployeeOffers()
    {
        var employeeId = _userContextService.GetUserId;
        var offers = await _dbContext.Offers
            .Include(o => o.Company)
            .Include(o => o.Technologies)
            .Where(o => o.CreatedById == employeeId).ToListAsync();
        var offerDtos = _mapper.Map<List<OfferDto>>(offers);
        return offerDtos;
    }


    public async Task DeleteAsManager(DeleteOfferDto dto)
    {
        var offer = await _dbContext.Offers.FirstOrDefaultAsync(o => o.Id == dto.Id || o.Name == dto.Name);
        if (offer is null) throw new NotFoundException("offer is not found");
        if ( _userContextService.GetUserRole != Role.ManagerCompanyAccount.ToString() && offer.CompanyId != _userContextService.GetCompanyId) throw new ForbidException();
        
            _dbContext.Offers.Remove(offer);
        await _dbContext.SaveChangesAsync();
    }
    public async Task DeleteAsEmployee(DeleteOfferDto dto)
    {
        var offer = await _dbContext.Offers.FirstOrDefaultAsync(o => o.Id == dto.Id || o.Name == dto.Name);
        if (offer is null) throw new NotFoundException("offer is not found");
        if (offer.CreatedById != _userContextService.GetUserId) throw new ForbidException();
        
        _dbContext.Offers.Remove(offer);
        await _dbContext.SaveChangesAsync();
    }
}
