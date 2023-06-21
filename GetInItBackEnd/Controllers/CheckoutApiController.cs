
using GetInItBackEnd.Entities;
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
        var paymentResult = await _paymentService.MakePayment();
        var url = paymentResult.Url;
        Response.Headers.Add("Location", url);
        return StatusCode(303);
    }

    [Authorize(Policy = "AdminRole")]
    [HttpPost("OfflinePayment")]
    public async Task<IActionResult> CreatePaymentOffline(OfflinePaymentDto dto)
    {
        await _paymentService.CreatePayment(dto);
        return Ok();
    }

    [Authorize(Policy = "AdminRole")]
    [HttpGet("GetEveryPayment")]
    public async Task<IEnumerable<PaymentDto>> GetEveryPayment()
    {
        return await _paymentService.GetEveryPayment();
    }

    [Authorize(Policy = "AdminRole")]
    [HttpGet("Payment/{id}")]
    public async Task<IActionResult> GetPaymentById(int id)
    {
        await _paymentService.GetByIdPayment(id);
        return Ok();
    }

    [HttpPost("PaymentToDataBase")]
    public async Task<IActionResult> PaymentToDataBase()
    {
        await _paymentService.SavePayment();
        return Ok();
    }
   
}

