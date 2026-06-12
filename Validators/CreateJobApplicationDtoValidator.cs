using FluentValidation;
using JobTracker.DTOs;

namespace JobTracker.Validators;

public class CreateJobApplicationDtoValidator : AbstractValidator<CreateJobApplicationDto>
{
    public CreateJobApplicationDtoValidator()
    {
        RuleFor(x => x.Company).NotEmpty().WithMessage("Company is required.").MaximumLength(100).WithMessage("Company name must not exceed 100 characters.");

        RuleFor(x => x.Position).NotEmpty().WithMessage("Position is required.").MaximumLength(100).WithMessage("Position must not exceed 100 characters.");

        RuleFor(x => x.JobUrl).Must(url => url == null || Uri.TryCreate(url, UriKind.Absolute, out _)).WithMessage("Job URL must be a valid URL.");
    }
}