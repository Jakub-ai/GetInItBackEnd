using FluentValidation;
using GetInItBackEnd.Entities;
using GetInItBackEnd.Models.JobApplicationDto;

namespace GetInItBackEnd.Models.Validators;

public class CreateJobApplicationDtoValidator : AbstractValidator<CreateJobApplicationDto>
{
    public CreateJobApplicationDtoValidator(GetInItDbContext dbContext)
    {
       // RuleFor(a => a.ResumePath).NotEmpty();
    }
}