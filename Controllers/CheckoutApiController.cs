using System.Collections.Generic;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Stripe;
using Stripe.Checkout;

namespace GetInItBackEnd.Controllers;

[Route("create-checkout-session")]
[ApiController]
public class CheckoutApiController : Controller
{
    [HttpPost]
    public ActionResult Create()
    {
        var domain = "http://localhost:3000";
        var options = new SessionCreateOptions
        {
            
            LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    // Provide the exact Price ID (for example, pr_1234) of the product you want to sell
                    Price = "price_1NEebZLpkQnyrIfCRA4cmJXA",
                    Quantity = 1,
                },
            },
            Mode = "payment",
            SuccessUrl = domain + "/paymentConfirmed",
            CancelUrl = domain + "/paymentRefused",
            AutomaticTax = new SessionAutomaticTaxOptions { Enabled = true },
        };
        var service = new SessionService();
        Session session = service.Create(options);

        Response.Headers.Add("Location", session.Url);
        return new StatusCodeResult(303);
    }
}