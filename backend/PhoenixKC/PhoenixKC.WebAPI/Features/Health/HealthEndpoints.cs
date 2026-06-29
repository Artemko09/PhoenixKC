using Mediator;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using PhoenixKC.WebAPI.Extensions;
using FluentResults.Extensions.AspNetCore;
using PhoenixKC.WebAPI.Features.Health.Dtos;
using PhoenixKC.WebAPI.Features.Health.Handlers;

namespace PhoenixKC.WebAPI.Features.Health;

public sealed class HealthEndpoints : IEndpointsProvider
{
    #region Static
    public static async Task<IResult> GetAllHealth(
        [FromServices] IMediator mediator, 
        CancellationToken cancellationToken
    )
    {
        Result<IEnumerable<HealthDto>> result = await mediator.Send(new GetAllHealthQuery(), cancellationToken);
        return result.ToActionResult().ToMvcResult();
    }
    public static async Task<IResult> GetHealth(
        [FromRoute] Guid id, 
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken
    )
    {
        Result<HealthDto> result = await mediator.Send(new GetHealthQuery(id), cancellationToken);
        return result.ToActionResult().ToMvcResult();
    }
    public static async Task<IResult> CreateHealth(
        [FromBody] HealthDto dto,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken
    )
    {
        Result result = await mediator.Send(new CreateHealthCommand(dto), cancellationToken);
        return result.ToActionResult().ToMvcResult();
    }
    public static async Task<IResult> UpdateHealth(
        [FromBody] HealthDto dto,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken
    )
    {
        Result result = await mediator.Send(new UpdateHealthCommand(dto), cancellationToken);
        return result.ToActionResult().ToMvcResult();
    }
    public static async Task<IResult> DeleteHealth(
        [FromRoute] Guid id,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken
    )
    {
        Result result = await mediator.Send(new DeleteHealthCommand(id), cancellationToken);
        return result.ToActionResult().ToMvcResult();
    }
    #endregion

    #region IEndpointsProvider
    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("/health");
        group.MapGet("/", GetAllHealth);
        group.MapGet("/{id:guid}", GetHealth);
        group.MapPost("/", CreateHealth);
        group.MapPut("/", UpdateHealth);
        group.MapDelete("/{id:guid}", DeleteHealth);
        return group;
    }
    #endregion
}