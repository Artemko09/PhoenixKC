using Mediator;
using FluentResults;

namespace PhoenixKC.WebAPI.Features.Health.Handlers;

public sealed record class DeleteHealthCommand(Guid Id) : IRequest<Result>;