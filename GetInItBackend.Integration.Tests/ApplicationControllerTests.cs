using System.Net;
using System.Text;
using GetInItBackEnd.Entities;
using GetInItBackEnd.Models.JobApplicationDto;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace GetInItBackend.Integration.Tests;

public class ApplicationControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ApplicationControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory
            .WithWebHostBuilder(b =>
            {
                b.ConfigureServices(s =>
                {
                    var dbOptions = s.SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<GetInItDbContext>));
                    s.AddSingleton<IPolicyEvaluator, FakePolicyUserEvaluator>();
                    s.AddMvc(o => o.Filters.Add(new FakeUserEvaluator()));

                    s.Remove(dbOptions);
                    s.AddDbContext<GetInItDbContext>(options => options.UseInMemoryDatabase("GetInItDb"));
                });
            }).CreateClient();
        
    }
    [Fact]
    public async Task GetAllApplications_ReturnsSuccessStatusCode()
    {
        var response = await _client.GetAsync("/api/JobApplications/GetAllApplications");

        response.EnsureSuccessStatusCode(); 
    }
    [Fact]
    public async Task DownloadResumeFile_ReturnsCorrectStatusCode()
    {
        // Arrange
        var fileDownloadDto = new FileDownloadDto
        {
            RelativePathFromDb = "C:\\Users\\jakub\\Desktop\\ISZProjekt\\GetInItBackEnd\\GetInItBackEnd\\wwwroot\\OfferFiles\\14" // You need to specify a valid file path here.
        };

        var content = new StringContent(
            JsonConvert.SerializeObject(fileDownloadDto), 
            Encoding.UTF8, 
            "application/json");

        // Act
        var response = await _client.PostAsync("/api/JobApplications/DownloadFile", content);

        // Assert
        if (File.Exists(fileDownloadDto.RelativePathFromDb))
        {
            Assert.True(response.IsSuccessStatusCode);
        }
        else
        {
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }

}
