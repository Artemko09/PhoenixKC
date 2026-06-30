using Mediator;
using FluentResults;
using PhoenixKC.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PhoenixKC.WebAPI.Features.Health.Dtos;

namespace PhoenixKC.WebAPI.Features.Health.Handlers;

public sealed class GetAllHealthHandler(PhoenixDbContext thisDbContext) : IRequestHandler<GetAllHealthQuery, Result<IEnumerable<HealthDto>>>
{
    #region IRequestHandler
    public async ValueTask<Result<IEnumerable<HealthDto>>> Handle(GetAllHealthQuery request, CancellationToken cancellationToken)
    {
        List<HealthDto> list = await thisDbContext.Health.AsNoTracking().ProjectToDto().ToListAsync(cancellationToken);
        return Result.Ok(list.AsEnumerable());
    }
    #endregion
}