using GetInItBackEnd.Models;
using GetInItBackEnd.Models.Offer;
using GetInItBackEnd.Services.OfferServices;
using Microsoft.AspNetCore.Authorization;
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

    [HttpGet("GetEveryExistingOffer")] 
    public async Task<IEnumerable<OfferDto>> GetAllOffers()
    {
        return await _offerService.GetEveryExistingOffer();
    }

    [HttpGet("SearchOffer")]
    public async Task<IEnumerable<OfferDto>> GetOffersByPrimarySkill([FromQuery] SearchOfferDto dto)
    {
        return await _offerService.SearchOffers(dto);
    }

    [HttpGet("getOffers")] 
    [Authorize(Policy = "EmployeeRole")]
    [Authorize(Policy = "ManagerRole")]
    public async Task<IEnumerable<TechnicalOfferDto>> GetOffers()
    {
        return await _offerService.GetAllOffers();
    }
    [HttpDelete("DeleteOffer")] 
    [Authorize(Policy = "EmployeeRole")]
    [Authorize(Policy = "ManagerRole")]
    public async Task<ActionResult> DeleteOffer([FromBody]DeleteOfferDto dto)
    {
        await _offerService.DeleteOffer(dto);
        return NoContent();
    }
    
    [HttpPut("updateOffer/{id}")]
    [Authorize("ManagerRole")]
    [Authorize("EmployeeRole")]
    public async Task<ActionResult> UpdateOffer([FromBody] UpdateOfferDto dto, [FromRoute] int id)
    {
        await _offerService.UpdateOffer(dto, id);
        return Ok();
    }

}