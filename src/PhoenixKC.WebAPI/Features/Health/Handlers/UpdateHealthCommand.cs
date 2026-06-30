using Mediator;
using FluentResults;
using PhoenixKC.WebAPI.Features.Health.Dtos;

namespace PhoenixKC.WebAPI.Features.Health.Handlers;

public sealed record class UpdateHealthCommand(HealthDto Health) : IRequest<Result>;