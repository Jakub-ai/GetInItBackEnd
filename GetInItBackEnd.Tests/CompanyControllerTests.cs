using AutoFixture;
using GetInItBackEnd.Controllers;
using GetInItBackEnd.Models.Company;
using GetInItBackEnd.Services.CompanyServices;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GetInItBackEnd.Tests;

public class CompanyControllerTests
{
    [Fact]
    public async Task GetAll_ReturnsCompanies_WhenCalled()
    {
   
        var fixture = new Fixture();
        var mockCompanyService = new Mock<ICompanyService>();

        var expected = fixture.CreateMany<CompanyDto>(5).ToList();

        mockCompanyService
            .Setup(service => service.GetAllCompanies())
            .ReturnsAsync(expected);

        var controller = new CompanyController(mockCompanyService.Object);

 
        var result = await controller.GetAll();

   
        Assert.Equal(expected, result);
    }
    
    [Fact]
    public async Task Update_ReturnsOk_WhenCalled()
    {
        // Arrange
        var fixture = new Fixture();
        var mockCompanyService = new Mock<ICompanyService>();
        var dto = fixture.Create<UpdateCompanyDto>();
        var id = 1;

        mockCompanyService
            .Setup(service => service.Update(id, dto))
            .Returns(Task.CompletedTask);

        var controller = new CompanyController(mockCompanyService.Object);

        // Act
        var result = await controller.Update(dto, id);

        // Assert
        Assert.IsType<OkResult>(result);
    }


}