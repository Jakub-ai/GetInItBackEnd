using GetInItBackEnd.Entities;

namespace GetInItBackEnd;

public class CompanySeeder
{
    private readonly GetInItDbContext _dbContext;

    public CompanySeeder(GetInItDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Seed()
    {
        if (await _dbContext.Database.CanConnectAsync())
        {
            if (!_dbContext.Accounts.Any())
            {
                /*var Admin = new Account
                {
                    Email = "admin@GetInIT.com",
                    Password = "123443",
                    Name = "John",
                    LastName = "Carter",
                    SubEndsAt = null,
                    Role = (Role)1,
                    Company = null,
                    Payments = null
                };*/
                /*var Payment = new Payment
                {
                    Id = 0,
                    PaymentDate = null,
                    Invoice = null,
                    Amount = 16.3m,
                    AccountId = 0,
                    Account = null
                }*/
                /*_dbContext.Accounts.Add(Admin);
                await _dbContext.SaveChangesAsync();*/


            }
        }
    }

}