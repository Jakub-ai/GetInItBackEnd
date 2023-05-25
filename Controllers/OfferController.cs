using GetInItBackEnd.Models;
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

    [HttpPost("api/offer/createOffer")]
    public async Task<ActionResult> CreateOffer([FromBody] CreateOfferDto dto)
    {
        var id = await _offerService.Create(dto);
        return Created($"a[i/offer/{id}", null);
    }
}