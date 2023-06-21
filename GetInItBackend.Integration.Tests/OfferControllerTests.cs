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
        // Arrange
        // Act
        var response = await _client.GetAsync("/api/offer/GetEveryExistingOffer");

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
    }

    [Fact]
    public async Task CreateOffer_ReturnCreatedCode()
    {
        var fixture = new Fixture();
        var mockService = new Mock<IAccountService>();
        var model = fixture.Create<CreateOfferDto>();
        var json = JsonConvert.SerializeObject(model);
        var httpContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

        // string jwtToken =
        //     "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjIiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA5LzA5L2lkZW50aXR5L2NsYWltcy9hY3RvciI6IjEiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiSmFrdWIiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJNYW5hZ2VyQ29tcGFueUFjY291bnQiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9zdXJuYW1lIjoiV29qIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZW1haWxhZGRyZXNzIjoibWFpbDFAbWFpbC5jb20iLCJleHAiOjE2ODg1NzI3MzIsImlzcyI6Imh0dHA6Ly9HZXRJbkl0LmNvbSIsImF1ZCI6Imh0dHA6Ly9HZXRJbkl0LmNvbSJ9.GUZNJzGeYKXJwMLwx5jOmY55nFGJUMCVlR8GiHD57b0";
        // _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
        var response = await _client.PostAsync("api/offer/createOffer", httpContent);

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        response.Headers.Location.Should().NotBeNull();
    }


    
}