using Mediator;
using FluentResults;
using PhoenixKC.Infrastructure;

namespace PhoenixKC.WebAPI.Features.Health.Handlers;

public sealed class CreateHealthHandler(PhoenixDbContext thisDbContext) : IRequestHandler<CreateHealthCommand, Result>
{
    #region IRequestHandler
    public async ValueTask<Result> Handle(CreateHealthCommand request, CancellationToken cancellationToken)
    {
        request.Health.Id = Guid.Empty;
        await thisDbContext.Health.AddAsync(request.Health.ToEntity(), cancellationToken);
        if(await thisDbContext.SaveChangesAsync(cancellationToken) == 0)
        {
            return Result.Fail("");
        }
        return Result.Ok();
    }
    #endregion
}