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
        var address = await _dbContext.Addresses.FirstOrDefaultAsync(a => a.Company.Id ==  (int)_userContextService.GetCompanyId);
        dto.City = address.City;
        var offer = _mapper.Map<Offer>(dto);
        offer.CreatedById = (int) _userContextService.GetUserId;
        offer.CompanyId =  (int)_userContextService.GetCompanyId;;
        
        await _dbContext.Offers.AddAsync(offer);
        await _dbContext.SaveChangesAsync();
        return offer.Id;
    }


    /*public async Task<IEnumerable<OfferDto>> GetByName(string name)
    {
        var lowerCaseName = name.ToLower();
        var offers = await _dbContext.Offers
            .Include(o => o.Company).ToListAsync();
        var offer = await _dbContext.Offers.FirstOrDefaultAsync(o => o.Name.ToLower().StartsWith(lowerCaseName));
        if (offer is null) throw new NotFoundException("Offer is not found");

        var result = _mapper.Map<List<OfferDto>>(offer);
        return result;

    }*/

    public async Task<IEnumerable<OfferDto>> GetOffers()
    {
        var offers = await _dbContext.Offers
            .Include(o => o.Company).ToListAsync();
        var offerDtos = _mapper.Map<List<OfferDto>>(offers);
        return offerDtos;
    }

    public async Task Delete(int id)
    {
        var offer = await _dbContext.Offers.FirstOrDefaultAsync(o => o.Id == id);
        if (offer is null) throw new NotFoundException("offer is not found");
        _dbContext.Offers.Remove(offer);
        await _dbContext.SaveChangesAsync();
    }

    private Task<Account?> GetUserByToken()
    {
        var account =  _dbContext.Accounts.FirstOrDefault(a => a.Id == _userContextService.GetUserId);
        return Task.FromResult(account);
    }
}