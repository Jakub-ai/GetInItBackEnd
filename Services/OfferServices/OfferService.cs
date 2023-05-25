using AutoMapper;
using GetInItBackEnd.Entities;
using GetInItBackEnd.Exceptions;
using GetInItBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace GetInItBackEnd.Services.OfferServices;

public class OfferService : IOfferService
{
    private readonly GetInItDbContext _dbContext;
    private readonly IMapper _mapper;

    public OfferService(GetInItDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<int> Create(CreateOfferDto dto)
    {
        var offer = _mapper.Map<Offer>(dto);
        await _dbContext.Offers.AddAsync(offer);
        await _dbContext.SaveChangesAsync();
        return offer.Id;
    }

    public async Task<OfferDto> GetByName(string name)
    {
        var offer = await _dbContext.Offers.FirstOrDefaultAsync(o => o.Name == name);
        if (offer is null) throw new NotFoundException("offer is not found");

        var result = _mapper.Map<OfferDto>(offer);
        return result;

    }

    public async Task<IEnumerable<OfferDto>> GetAll()
    {
        var offers = await _dbContext.Offers.ToListAsync();
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
}