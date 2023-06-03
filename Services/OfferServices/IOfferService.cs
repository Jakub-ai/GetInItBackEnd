using GetInItBackEnd.Models;
using GetInItBackEnd.Models.Offer;

namespace GetInItBackEnd.Services.OfferServices;

public interface IOfferService
{
    Task<int> Create(CreateOfferDto dto);
   // Task<OfferDto> GetByName(string name);
    Task<IEnumerable<OfferDto>> GetOffers();
    public Task DeleteAsManager(DeleteOfferDto dto);
    public Task DeleteAsEmployee(DeleteOfferDto dto);
    public Task<IEnumerable<OfferDto>> GetByName(string name);
    public Task<IEnumerable<OfferDto>> GetByTechnology(string tech);
    public Task<IEnumerable<OfferDto>> GetByPrimarySkill(string primarySkill);
    public Task<IEnumerable<OfferDto>> GetAllCompanyOffers();
    public Task<IEnumerable<OfferDto>> GetAllEmployeeOffers();
}