using Mediator;
using FluentResults;
using PhoenixKC.WebAPI.Features.Health.Dtos;

namespace PhoenixKC.WebAPI.Features.Health.Handlers;

public sealed record class GetHealthQuery(Guid Id) : IRequest<Result<HealthDto>>;