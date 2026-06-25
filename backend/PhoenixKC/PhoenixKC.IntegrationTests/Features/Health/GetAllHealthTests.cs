using FluentResults;
using FluentAssertions;
using System.Net.Http.Json;
using PhoenixKC.Infrastructure;
using PhoenixKC.WebAPI.Features.Health;
using PhoenixKC.WebAPI.Features.Health.Dtos;
using Microsoft.Extensions.DependencyInjection;
using PhoenixKC.Infrastructure.Features.Health;
using Mediator;
using PhoenixKC.WebAPI.Features.Health.Handlers;

namespace PhoenixKC.IntegrationTests.Features.Health;

public sealed class GetAllHealthTests(PhoenixWebApplicationFactory thisFactory) : IClassFixture<PhoenixWebApplicationFactory>
{
    [Fact]
    public async Task GetAllHealth_ShouldSucceded_WhenHealthIsEmpty()
    {
        //Arrange
        using IServiceScope handler_scope = thisFactory.Services.CreateScope();
        IMediator mediator = handler_scope.ServiceProvider.GetRequiredService<IMediator>();
        HealthEntity entity = new()
        {
            Name = "TestHealth"
        };
        using(IServiceScope db_scope = thisFactory.Services.CreateScope())
        {
            PhoenixDbContext db_context = db_scope.ServiceProvider.GetRequiredService<PhoenixDbContext>();
            await db_context.Health.AddAsync(entity, CancellationToken.None);
            await db_context.SaveChangesAsync(CancellationToken.None);
        }
        HealthDto dto = entity.ToDto();

        //Act
        Result<IEnumerable<HealthDto>> result = await mediator.Send(new GetAllHealthQuery(), CancellationToken.None);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo([dto]);
    }
}