using FluentValidation;
using PhoenixKC.WebAPI.Features.Health.Handlers;

namespace PhoenixKC.WebAPI.Features.Health.Validators;

public sealed class CreateHealthValidator : AbstractValidator<CreateHealthCommand>
{
    public CreateHealthValidator()
    {
        base.RuleFor(c => c.Health).SetValidator(new HealthDtoValidator());
    }
}