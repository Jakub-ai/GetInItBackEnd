using System.Net.Http.Headers;
using System.Text;
using AutoFixture;
using FluentAssertions;
using FluentAssertions.Common;
using GetInItBackEnd.Entities;
using GetInItBackEnd.Models;
using GetInItBackEnd.Models.Account;
using GetInItBackEnd.Services.AccountServices;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using Newtonsoft.Json;

namespace GetInItBackend.Integration.Tests;

public class OfferControllerTests : IClassFixture<WebApplicationFactory<Program>>
{

    private readonly HttpClient _client;


    public OfferControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory
            .WithWebHostBuilder(b =>
            {
                b.ConfigureServices(s =>
                {
                    var dbOptions = s.SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<GetInItDbContext>));
                    s.AddSingleton<IPolicyEvaluator, FakePolicyManagerEvaluator>();
                    s.AddMvc(o => o.Filters.Add(new FakeManagerFilter()));

                    s.Remove(dbOptions);
                    s.AddDbContext<GetInItDbContext>(options => options.UseInMemoryDatabase("GetInItDb"));
                });
            }).CreateClient();
        
    }
    [Fact]
    public async Task GetAllOffers_ReturnsSuccessStatusCode()
    {
        
        var response = await _client.GetAsync("/api/offer/GetEveryExistingOffer");


        response.EnsureSuccessStatusCode(); // Status Code 200-299
    }



    
}