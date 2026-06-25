using Mediator;
using FluentValidation;
using FluentValidation.Results;

namespace PhoenixKC.WebAPI.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> thisValidators
) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull, IMessage
{
    #region IPipelineBehavior
    public async ValueTask<TResponse> Handle(
        TRequest request,
        MessageHandlerDelegate<TRequest, TResponse> next,
        CancellationToken cancellationToken
    )
    {
        if(!thisValidators.Any())
        {
            return await next(request, cancellationToken);
        }
        ValidationContext<TRequest> context = new(request);
        ValidationResult[] results = await Task.WhenAll(
            thisValidators.Select(v => v.ValidateAsync(context, cancellationToken))
        );
        List<ValidationFailure> failures = results.SelectMany(r => r.Errors).ToList();
        if(failures.Count != 0)
        {
            throw new ValidationException(failures);
        }
        return await next(request, cancellationToken);
    }
    #endregion
}