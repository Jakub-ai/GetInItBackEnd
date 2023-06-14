using System.Collections.Generic;
using Azure;
using GetInItBackEnd.Entities;
using GetInItBackEnd.Models.PaymentsDtos;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.HttpResults;
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
        var payment =  _dbContext.Payments;
        var domain = "http://localhost:3000";
        var options = new SessionCreateOptions
        {
            
            LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    // Provide the exact Price ID (for example, pr_1234) of the product you want to sell
                    Price = "price_1NG1yHLpkQnyrIfC1TdGrsY3",
                    Quantity = 1,
                }
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

    /*public async Task<int> PaymentToDatabase()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

        try
        {
            var stripeEvent = EventUtility.ParseEvent(json);

            // Handle the event
            if (stripeEvent.Type == Events.PaymentIntentSucceeded)
            {
                var paymentIntent = stripeEvent.Data.Object as PaymentIntent;

                // Create a new DTO with the required fields
                var paymentDto = new CreatePaymentDto
                {
                    PaymentDate = DateTime.Now.ToString(), // Set the payment date to the current date and time
                    Amount = paymentIntent.Amount /
                             100M, // Stripe amounts are in cents, so we need to convert it to dollars
                    StripePaymentId = paymentIntent.Id,
                    PaymentStatus = "Succeeded"
                };

                // Convert the DTO to a Payment object
                var payment = new Payment
                {
                    PaymentDate = ,
                    Amount = paymentDto.Amount,
                    StripePaymentId = paymentDto.StripePaymentId,
                    PaymentStatus = paymentDto.PaymentStatus
                };

                // Add the new payment to the database
                _dbContext.Payments.Add(payment);
                await _dbContext.SaveChangesAsync();
            }

            return
        }
    }*/
}