using FluentValidation;
using GetInItBackEnd.Entities;
using GetInItBackEnd.Models.Account;

namespace GetInItBackEnd.Models.Validators;

public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
{
    public RegisterUserDtoValidator(GetInItDbContext dbContext)
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
        RuleFor(x => x.Email)
            .Custom((value, context) =>
            {
                var emailInUse = dbContext.Accounts.Any(u => u.Email == value);
                if (emailInUse)
                {
                    context.AddFailure("Email", "Email is taken!");
                }
            });
        RuleFor(x => x.Password)
            .MinimumLength(6);
        RuleFor(x => x.ConfirmPassword).Equal(e => e.Password);
    }
}