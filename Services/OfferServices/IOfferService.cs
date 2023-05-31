using GetInItBackEnd.Models;
using GetInItBackEnd.Models.Offer;

namespace GetInItBackEnd.Services.OfferServices;

public interface IOfferService
{
    Task<int> Create(CreateOfferDto dto);
   // Task<OfferDto> GetByName(string name);
    Task<IEnumerable<OfferDto>> GetOffers();
    Task Delete(int id);
}