using System.Collections.Generic;
using AutoMapper;
using Azure;
using GetInItBackEnd.Entities;
using GetInItBackEnd.Models.PaymentsDtos;
using GetInItBackEnd.Services.AccountServices;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Stripe;
using Stripe.Checkout;


namespace GetInItBackEnd.Services.PaymentServices;

public class PaymentService : IPaymentService
{
    private readonly GetInItDbContext _dbContext;
    private readonly ILogger _logger;
    private readonly IUserContextService _userContextService;
    private readonly IMapper _mapper;

    public PaymentService(GetInItDbContext dbContext, ILogger<PaymentService> logger, IUserContextService userContextService, IMapper mapper)
    {
        _dbContext = dbContext;
        _logger = logger;
        _userContextService = userContextService;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PaymentDto>> GetEveryPayment()
    {
       var payments = await _dbContext.Payments.ToListAsync();
       var result = _mapper.Map<List<PaymentDto>>(payments);
       return result;
    }

    public async Task<PaymentDto> GetByIdPayment(int id)
    {
        var payment = await _dbContext.Payments.FirstOrDefaultAsync(p => p.Id == id);
        var result = _mapper.Map<PaymentDto>(payment);
        return result;
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
                   
                    Price = "price_1NL27sLpkQnyrIfCRTRz8g91",
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

    public async Task<int> SavePayment()
    {
        var paymentDto =  new PaymentDto
        {
            Name = _userContextService.GetUserName,
            Email = _userContextService.GetUserMail,
            PaymentDate = DateTime.UtcNow,
            Amount = "15",
            StripePaymentId = new Random().Next(100000).ToString(),
            PaymentStatus = "Paid"
        };
        var paymentToDataBase = _mapper.Map<Payment>(paymentDto);
        await _dbContext.Payments.AddAsync(paymentToDataBase);
        await _dbContext.SaveChangesAsync();
        return paymentToDataBase.Id;
    }

    public async Task<int> CreatePayment(OfflinePaymentDto dto)
    {
        var payment = _mapper.Map<Payment>(dto);
        payment.PaymentStatus = "Offline Payment";
        payment.PaymentDate = DateTime.UtcNow;
        await _dbContext.Payments.AddAsync(payment);
        await _dbContext.SaveChangesAsync();
        return payment.Id;
    }
    

    /*public async Task<int> PaymentToDatabase(HttpRequest request)
    {
        var json = await new StreamReader(request.Body).ReadToEndAsync();

        try
        {
            var stripeEvent = EventUtility.ParseEvent(json);

            if (stripeEvent.Type == Events.PaymentIntentSucceeded)
            {
                var paymentIntent = stripeEvent.Data.Object as PaymentIntent;

                var paymentDto = new PaymentDto
                {
                    
                    PaymentDate = DateTime.UtcNow,
                    Amount = paymentIntent.Amount.ToString() ,
                    StripePaymentId = paymentIntent.Id,
                    PaymentStatus = "Succeeded",
                    Name = _userContextService.GetUserName,
                    LastName = _userContextService.GetUserLastName

                };

                var payment = new Payment
                {
                    PaymentDate = paymentDto.PaymentDate,
                    Amount = paymentDto.Amount,
                    StripePaymentId = paymentDto.StripePaymentId,
                    PaymentStatus = paymentDto.PaymentStatus,
                    Name = paymentDto.Name,
                    Email = paymentDto.Email
                    
                    
                };

                _dbContext.Payments.Add(payment);
                await _dbContext.SaveChangesAsync();

                return 1; // W przypadku sukcesu, zwróć 1
            }

            return 0; // W przypadku niepowodzenia, zwróć 0
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return -1; // W przypadku błędu, zwróć -1
        }
    }
    */

}
