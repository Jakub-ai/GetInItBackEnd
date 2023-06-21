using AutoFixture;
using GetInItBackEnd.Controllers;
using GetInItBackEnd.Models.Offer;
using GetInItBackEnd.Services.OfferServices;
using Moq;

namespace GetInItBackEnd.Tests;

public class OfferControllerTests
{
    [Fact]
    public async Task GetOffersByPrimarySkill_ReturnsOffers_WhenCalled()
    {
        
        var fixture = new Fixture();
        var mockOfferService = new Mock<IOfferService>();
        var dto = fixture.Create<SearchOfferDto>();

        var expected = fixture.CreateMany<OfferDto>(2).ToList();

        mockOfferService
            .Setup(service => service.SearchOffers(dto))
            .ReturnsAsync(expected);

        var controller = new OfferController(mockOfferService.Object);

    
        var result = await controller.GetOffersByPrimarySkill(dto);

      
        Assert.Equal(expected, result);
    }
    [Fact]
    public async Task GetAllOffers_ReturnsOffers_WhenCalled()
    {

        var fixture = new Fixture();
        var mockOfferService = new Mock<IOfferService>();

        var expected = fixture.CreateMany<OfferDto>(5).ToList();

        mockOfferService
            .Setup(service => service.GetEveryExistingOffer())
            .ReturnsAsync(expected);

        var controller = new OfferController(mockOfferService.Object);

     
        var result = await controller.GetAllOffers();

       
        Assert.Equal(expected, result);
    }


}