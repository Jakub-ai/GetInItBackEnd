using AutoFixture;
using GetInItBackEnd.Controllers;
using GetInItBackEnd.Entities;
using GetInItBackEnd.Models.Account;
using GetInItBackEnd.Models.Address;
using GetInItBackEnd.Models.Company;
using GetInItBackEnd.Services.AccountServices;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GetInItBackEnd.Tests;

public class AccountControllerTests
{
    [Fact]
    public async Task RegisterCompanyAccount_ReturnsCreatedResult_WhenDtoIsValid()
    {
        // Arrange
        var fixture = new Fixture();
        var mockService = new Mock<IAccountService>();

        var validAccountDto = fixture.Create<CreateAccountDto>();

        var createdId = 1;

        mockService.Setup(s => s.RegisterCompanyAccount(It.IsAny<CreateAccountDto>())).ReturnsAsync(createdId);

        var controller = new AccountController(mockService.Object);

        // Act
        var result = await controller.RegisterCompanyAccount(validAccountDto);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedResult>(result);
        Assert.Equal($"/api/account/{createdId}", createdAtActionResult.Location);
    }

    
    [Fact]
    public async Task RegisterUserAccount_ReturnsCreatedResult_WhenDtoIsValid()
    {
        // Arrange
        var fixture = new Fixture();
        var mockService = new Mock<IAccountService>();

        var validUserDto = fixture.Create<RegisterUserDto>();

        var createdId = 1;

        mockService.Setup(s => s.RegisterUser(It.IsAny<RegisterUserDto>())).ReturnsAsync(createdId);

        var controller = new AccountController(mockService.Object);
        
        var result = await controller.RegisterUserAccount(validUserDto);

 
        var createdAtActionResult = Assert.IsType<CreatedResult>(result);
        Assert.Equal($"/api/account/{createdId}", createdAtActionResult.Location);
    }


}