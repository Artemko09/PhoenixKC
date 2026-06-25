using Mediator;
using FluentResults;
using PhoenixKC.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PhoenixKC.Infrastructure.Features.Health;

namespace PhoenixKC.WebAPI.Features.Health.Handlers;

public sealed class DeleteHealthHandler(PhoenixDbContext thisDbContext) : IRequestHandler<DeleteHealthCommand, Result>
{
    #region IRequestHandler
    public async ValueTask<Result> Handle(DeleteHealthCommand request, CancellationToken cancellationToken)
    {
        HealthEntity? entity = await thisDbContext.Health.FirstOrDefaultAsync(h => h.Id == request.Id, cancellationToken);
        if(entity is null)
        {
            return Result.Fail("");
        }
        thisDbContext.Health.Remove(entity);
        if(await thisDbContext.SaveChangesAsync(cancellationToken) == 0)
        {
            return Result.Fail("");
        }
        return Result.Ok();
    }
    #endregion
}