using System.Collections.Generic;
using Azure;
using GetInItBackEnd.Entities;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Stripe;
using Stripe.Checkout;


namespace GetInItBackEnd.Services.PaymentServices;

public class PaymentService : IPaymentService
{
    private readonly GetInItDbContext _dbContext;

    public PaymentService(GetInItDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Session> MakePayment()
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
        Session session = await service.CreateAsync(options);
        return session;


    }
}