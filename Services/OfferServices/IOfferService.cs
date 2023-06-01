using GetInItBackEnd.Models;
using GetInItBackEnd.Models.Offer;

namespace GetInItBackEnd.Services.OfferServices;

public interface IOfferService
{
    Task<int> Create(CreateOfferDto dto);
   // Task<OfferDto> GetByName(string name);
    Task<IEnumerable<OfferDto>> GetOffers();
    Task Delete(int id);
    public Task<IEnumerable<OfferDto>> GetByName(string name);
    public Task<IEnumerable<OfferDto>> GetByTechnology(string tech);
    public Task<IEnumerable<OfferDto>> GetByPrimarySkill(string primarySkill);
    public Task<IEnumerable<OfferDto>> GetAllCompanyOffers();
    public Task<IEnumerable<OfferDto>> GetAllEmployeeOffers();
}