using FluentValidation;
using PhoenixKC.WebAPI.Features.Health.Handlers;

namespace PhoenixKC.WebAPI.Features.Health.Validators;

public sealed class UpdateHealthValidator : AbstractValidator<UpdateHealthCommand>
{
    public UpdateHealthValidator()
    {
        base.RuleFor(c => c.Health).SetValidator(new HealthDtoValidator());
    }
}