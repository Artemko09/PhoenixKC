using Mediator;
using FluentResults;
using PhoenixKC.Infrastructure;
using PhoenixKC.WebAPI.Resources;
using PhoenixKC.WebAPI.Extensions;
using Microsoft.EntityFrameworkCore;
using PhoenixKC.Infrastructure.Features.Health;

namespace PhoenixKC.WebAPI.Features.Health.Handlers;

public sealed class UpdateHealthHandler(
    PhoenixDbContext thisDbContext,
    ILogger<UpdateHealthHandler> thisLogger
) : IRequestHandler<UpdateHealthCommand, Result>
{
    #region IRequestHandler
    public async ValueTask<Result> Handle(UpdateHealthCommand request, CancellationToken cancellationToken)
    {
        HealthEntity? entity = await thisDbContext.Health.FirstOrDefaultAsync(h => h.Id == request.Health.Id, cancellationToken);
        if(entity is null)
        {
            return thisLogger.LogFailResult(ErrorMessages.RecordNotFound, nameof(HealthEntity.Id), request.Health.Id);
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