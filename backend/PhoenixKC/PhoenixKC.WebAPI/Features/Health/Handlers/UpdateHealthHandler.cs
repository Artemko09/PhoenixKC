using Mediator;
using FluentResults;
using PhoenixKC.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PhoenixKC.Infrastructure.Features.Health;

namespace PhoenixKC.WebAPI.Features.Health.Handlers;

public sealed class UpdateHealthHandler(PhoenixDbContext thisDbContext) : IRequestHandler<UpdateHealthCommand, Result>
{
    #region IRequestHandler
    public async ValueTask<Result> Handle(UpdateHealthCommand request, CancellationToken cancellationToken)
    {
        HealthEntity? entity = await thisDbContext.Health.FirstOrDefaultAsync(h => h.Id == request.Health.Id, cancellationToken);
        if(entity is null)
        {
            return Result.Fail("");
        }
        request.Health.MapToEntity(entity);
        if(await thisDbContext.SaveChangesAsync(cancellationToken) == 0)
        {
            return Result.Fail("");
        }
        return Result.Ok();
    }
    #endregion
}