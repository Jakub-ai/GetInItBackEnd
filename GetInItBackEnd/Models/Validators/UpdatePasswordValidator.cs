using FluentValidation;
using GetInItBackEnd.Entities;
using GetInItBackEnd.Models.Account;

namespace GetInItBackEnd.Models.Validators;

public class UpdatePasswordValidator : AbstractValidator<UpdatePasswordDto>
{
    public UpdatePasswordValidator(GetInItDbContext dbContext )
    {
        RuleFor(x => x.Password)
            .NotEmpty()
            .Custom((value, context) =>
            {
                var passwordInUse = dbContext.Accounts.Any(a => a.PasswordHash == value);
                if (passwordInUse)
                {
                    context.AddFailure("Password", "Password is the same");
                }
            });
        RuleFor(e => e.ConfirmPassword)
            .Equal(u => u.Password);
    }
}