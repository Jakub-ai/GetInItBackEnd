using AutoFixture;
using GetInItBackEnd.Controllers;
using GetInItBackEnd.Entities;
using GetInItBackEnd.Models.Account;
using GetInItBackEnd.Models.Address;
using GetInItBackEnd.Models.Company;
using GetInItBackEnd.Services.AccountServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace GetInItBackEnd.Tests;

public class AccountControllerTests
{
    [Fact]
    public async Task RegisterCompanyAccount_ReturnsCreatedResult_WhenDtoIsValid()
    {
   
        var fixture = new Fixture();
        var mockService = new Mock<IAccountService>();

        var validAccountDto = fixture.Create<CreateAccountDto>();

        var createdId = 1;

        mockService.Setup(s => s.RegisterCompanyAccount(It.IsAny<CreateAccountDto>())).ReturnsAsync(createdId);

        var controller = new AccountController(mockService.Object);

       
        var result = await controller.RegisterCompanyAccount(validAccountDto);


        var createdAtActionResult = Assert.IsType<CreatedResult>(result);
        Assert.Equal($"/api/account/{createdId}", createdAtActionResult.Location);
    }

    
    [Fact]
    public async Task RegisterUserAccount_ReturnsCreatedResult_WhenDtoIsValid()
    {
     
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
    [Fact]
    public async Task ChangePassword_ReturnsOk_WhenCalled()
    {
       
        var mockAccountService = new Mock<IAccountService>();
        var dto = new UpdatePasswordDto { Password = "NewPassword", ConfirmPassword = "NewPassword" };
    
        mockAccountService
            .Setup(service => service.ChangePassword(dto))
            .Returns(Task.CompletedTask);

        var controller = new AccountController(mockAccountService.Object);

     
        var result = await controller.ChangePassword(dto);

        
        Assert.IsType<OkResult>(result);
    }
    [Fact]
    public async Task ChangeEmail_ReturnsOk_WhenCalled()
    {
      
        var mockAccountService = new Mock<IAccountService>();
        var dto = new UpdateEmailDto { Email = "newEmail@example.com", ConfirmEmail = "newEmail@example.com" };
    
        mockAccountService
            .Setup(service => service.ChangeEmail(dto))
            .Returns(Task.CompletedTask);

        var controller = new AccountController(mockAccountService.Object);

        
        var result = await controller.ChangeEmail(dto);

       
        Assert.IsType<OkResult>(result);
    }




}