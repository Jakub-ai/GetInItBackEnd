using System.Collections.Generic;
using GetInItBackEnd.Services.PaymentServices;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Stripe;
using Stripe.Checkout;

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
    public Task<ActionResult> MakePayment()
    {
        var url = _paymentService.MakePayment().Result.Url;
        Response.Headers.Add("Location",  url);
        return Task.FromResult<ActionResult>(new StatusCodeResult(303));
    }
    [HttpPost("webhook")]
    public async Task<IActionResult> HandleStripeWebhook()
    {
    

        // Return a response to Stripe to acknowledge receipt of the event
        return Ok();
    }

}