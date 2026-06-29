using Mediator;
using FluentResults;
using PhoenixKC.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PhoenixKC.WebAPI.Features.Health.Dtos;
using PhoenixKC.Infrastructure.Features.Health;

namespace PhoenixKC.WebAPI.Features.Health.Handlers;

public sealed class GetHealthHandler(PhoenixDbContext thisDbContext) : IRequestHandler<GetHealthQuery, Result<HealthDto>>
{
    #region IRequestHandler
    public async ValueTask<Result<HealthDto>> Handle(GetHealthQuery request, CancellationToken cancellationToken)
    {
        HealthEntity? entity = await thisDbContext.Health.AsNoTracking().FirstOrDefaultAsync(h => h.Id == request.Id, cancellationToken);
        if(entity is null)
        {
            return Result.Fail("");
        }
        return Result.Ok(entity.ToDto());
    }
    #endregion
}