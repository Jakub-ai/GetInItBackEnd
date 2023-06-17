
using GetInItBackEnd.Models.PaymentsDtos;
using GetInItBackEnd.Services.PaymentServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace GetInItBackEnd.Controllers;

[Route("CreateCheckoutSession")]
[ApiController]
public class CheckoutApiController : Controller
{
    private readonly IPaymentService _paymentService;

    public CheckoutApiController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }
    [HttpPost("Payment")]
    public async Task<ActionResult> MakePayment()
    {
        var url = _paymentService.MakePayment().Result.Url;
        Response.Headers.Add("Location",  url);
        return await Task.FromResult<ActionResult>(new StatusCodeResult(303));
    }

    [Authorize(Policy = "AdminRole")]
    [HttpPost("OfflinePayment")]
    public async Task<IActionResult> CreatePaymentOffline(CreatePaymentDto dto)
    {
        await _paymentService.CreatePayment(dto);
        return Ok();
    }
    [HttpPost("webhook")]
    public async Task<IActionResult> HandleStripeWebhook()
    {
        
        var result = await _paymentService.PaymentToDatabase(Request);

        if (result == 1)
        {
            return Ok();
        }
        else if (result == -1)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return BadRequest();
    }
}

