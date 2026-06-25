using Mediator;
using FluentResults;
using PhoenixKC.WebAPI.Features.Health.Dtos;

namespace PhoenixKC.WebAPI.Features.Health.Handlers;

public sealed record class CreateHealthCommand(HealthDto Health) : IRequest<Result>;