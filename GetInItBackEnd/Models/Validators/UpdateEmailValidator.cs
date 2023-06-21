using FluentValidation;
using GetInItBackEnd.Entities;
using GetInItBackEnd.Models.Account;

namespace GetInItBackEnd.Models.Validators;

public class UpdateEmailValidator : AbstractValidator<UpdateEmailDto>
{
    public UpdateEmailValidator(GetInItDbContext dbContext )
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .Custom((value, context) =>
            {
                var emailInUse = dbContext.Accounts.Any(a => a.Email == value);
                if (emailInUse)
                {
                    context.AddFailure("Email", "email is taken");
                }
            });
        RuleFor(e => e.ConfirmEmail)
            .Equal(u => u.Email);
        
    }
}