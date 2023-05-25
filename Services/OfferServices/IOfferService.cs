using GetInItBackEnd.Models;

namespace GetInItBackEnd.Services.OfferServices;

public interface IOfferService
{
    Task<int> Create(CreateOfferDto dto);
    Task<OfferDto> GetByName(string name);
    Task<IEnumerable<OfferDto>> GetAll();
    Task Delete(int id);
}