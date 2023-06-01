using GetInItBackEnd.Models;
using GetInItBackEnd.Models.Offer;
using GetInItBackEnd.Services.OfferServices;
using Microsoft.AspNetCore.Mvc;

namespace GetInItBackEnd.Controllers;
[Route("api/offer")]
public class OfferController : ControllerBase
{
    private readonly IOfferService _offerService;

    public OfferController(IOfferService offerService)
    {
        _offerService = offerService;
    }

    [HttpPost("createOffer")]
    public async Task<ActionResult> CreateOffer([FromBody] CreateOfferDto dto)
    {
        var id = await _offerService.Create(dto);
        return Created($"api/offer/{id}", null);
    }

    [HttpGet("GetAllOffers")] 
    public async Task<IEnumerable<OfferDto>> GetAllOffers()
    {
        return await _offerService.GetOffers();
    }
    [HttpGet("GetOfferByName")] 
    public async Task<IEnumerable<OfferDto>> GetOffersByName([FromQuery]string name)
    {
        return await _offerService.GetByName(name);
    }
    [HttpGet("GetOfferByTech")] 
    public async Task<IEnumerable<OfferDto>> GetOffersByTechnology([FromQuery]string name)
    {
        return await _offerService.GetByTechnology(name);
    }
    [HttpGet("GetOfferByPrimarySkill")] 
    public async Task<IEnumerable<OfferDto>> GetOffersByPrimarySkill([FromQuery]string name)
    {
        return await _offerService.GetByPrimarySkill(name);
    }

}