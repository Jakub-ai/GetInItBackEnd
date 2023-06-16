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
    public async Task<IEnumerable<OfferDto>> SearchOffers(SearchOfferDto dto)
    {
        IQueryable<Offer> offers = _dbContext.Offers
            .Include(o => o.Company)
            .Include(o => o.Technologies);

        if (!string.IsNullOrEmpty(dto.Name))
        {
            var lowerCaseName = dto.Name.ToLower();
            offers = offers.Where(o => o.Name.ToLower().StartsWith(lowerCaseName));
        }
    
        if (!string.IsNullOrEmpty(dto.PrimarySkill))
        {
            var lowerCaseSkill = dto.PrimarySkill.ToLower();
            offers = offers.Where(o => o.PrimarySkill.ToLower().StartsWith(lowerCaseSkill));
        }
    
        if (!string.IsNullOrEmpty(dto.CompanyName))
        {
            var lowerCaseCompanyName = dto.CompanyName.ToLower();
            offers = offers.Where(o => o.Company.Name.ToLower().StartsWith(lowerCaseCompanyName));
        }

        if (dto.Technologies != null && dto.Technologies.Any())
        {
            foreach(var technology in dto.Technologies)
            {
                var lowerCaseTech = technology.Skill.ToLower();
                offers = offers.Where(o => o.Technologies.Any(t => t.Skill.ToLower() == lowerCaseTech));
            }
        }

        if (!string.IsNullOrEmpty(dto.City))
        {
            var lowerCaseCity = dto.City.ToLower();
            offers = offers.Where(o => o.City.ToLower() == lowerCaseCity);
        }

        if (dto.Level.HasValue)
        {
            offers = offers.Where(o => o.Level == dto.Level.Value);
        }

        if (dto.Place.HasValue)
        {
            offers = offers.Where(o => o.Place == dto.Place.Value);
        }

        var resultList = await offers.ToListAsync();

        if (!resultList.Any())
            throw new NotFoundException("Offer is not found");

        var result = _mapper.Map<List<OfferDto>>(resultList);

        return result;
    }



    public async Task<IEnumerable<OfferDto>> GetEveryExistingOffer()
    {
        var offers = await _dbContext.Offers
            .Include(o => o.Company)
            .Include(t => t.Technologies).ToListAsync();
        var offerDtos = _mapper.Map<List<OfferDto>>(offers);
        return offerDtos;
    }
    
    public async Task<IEnumerable<OfferDto>> GetAllOffers()
    {
        var role = _userContextService.GetUserRole;
        var userId = _userContextService.GetUserId;
        var companyId = _userContextService.GetCompanyId;

        IQueryable<Offer> offers = _dbContext.Offers
            .Include(o => o.Company)
            .Include(o => o.Technologies);

        if (role == Role.EmployeeAccount.ToString())
        {
            offers = offers.Where(o => o.CreatedById == userId);
        }
        else if (role == Role.ManagerCompanyAccount.ToString())
        {
            offers = offers.Where(o => o.CompanyId == companyId);
        }
        else
        {
            throw new UnauthorizedAccessException("User role not allowed");
        }

        var offerDtos = _mapper.Map<List<OfferDto>>(await offers.ToListAsync());

        return offerDtos;
    }
    public async Task UpdateOffer(UpdateOfferDto dto, int id)
    {
        var userId = _userContextService.GetUserId;
        var companyId = _userContextService.GetCompanyId;
        var role = _userContextService.GetUserRole;
        Offer? offer;

        if (role == Role.EmployeeAccount.ToString())
        {
            var employee = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.Id == userId && role == Role.EmployeeAccount.ToString());
            if (employee is null) throw new NotFoundException("Employee not found");

            offer = await _dbContext.Offers
                .Include(o => o.Company)
                .Include(o => o.Technologies)
                .FirstOrDefaultAsync(o => o.CreatedById == userId && o.Id == id);
        }
        else if (role == Role.ManagerCompanyAccount.ToString())
        {
            var manager = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.Id == userId && role == Role.ManagerCompanyAccount.ToString());
            if (manager is null) throw new NotFoundException("Manager not found");
            offer = await _dbContext.Offers
                .Include(o => o.Company)
                .Include(o => o.Technologies)
                .FirstOrDefaultAsync(o => o.CompanyId == companyId && o.Id == id);
        }
        else
        {
            throw new UnauthorizedAccessException("User role not allowed");
        }

        if (offer is null) throw new NotFoundException("offer is not found");

        _mapper.Map(dto, offer);

        await _dbContext.SaveChangesAsync();
    }
  
    public async Task DeleteOffer(DeleteOfferDto dto)
    {
        var offer = await _dbContext.Offers.FirstOrDefaultAsync(o => o.Id == dto.Id);
        if (offer is null) throw new NotFoundException("Offer is not found");
    
        var role = _userContextService.GetUserRole;
        var userId = _userContextService.GetUserId;
        var companyId = _userContextService.GetCompanyId;

        if ((role == Role.EmployeeAccount.ToString() && offer.CreatedById == userId) ||
            (role == Role.ManagerCompanyAccount.ToString() && offer.CompanyId == companyId))
        {
            _dbContext.Offers.Remove(offer);
            await _dbContext.SaveChangesAsync();
        }
        else
        {
            throw new ForbidException();
        }
    }

}
