using FluentValidation;
using PhoenixKC.WebAPI.Features.Health.Dtos;
using PhoenixKC.Infrastructure.Features.Health;

namespace PhoenixKC.WebAPI.Features.Health.Validators;

public sealed class HealthDtoValidator : AbstractValidator<HealthDto>
{
    public HealthDtoValidator()
    {
        base.RuleFor(h => h.Name).MaximumLength(HealthConstants.NameMaxLength);
    }
}