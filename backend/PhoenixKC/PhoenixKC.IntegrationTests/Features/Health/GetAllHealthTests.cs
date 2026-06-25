using Mediator;
using FluentResults;
using FluentAssertions;
using PhoenixKC.Infrastructure;
using PhoenixKC.WebAPI.Features.Health;
using PhoenixKC.WebAPI.Features.Health.Dtos;
using Microsoft.Extensions.DependencyInjection;
using PhoenixKC.Infrastructure.Features.Health;
using PhoenixKC.WebAPI.Features.Health.Handlers;

namespace PhoenixKC.IntegrationTests.Features.Health;

public sealed class GetAllHealthTests(PhoenixWebApplicationFactory thisFactory) : IClassFixture<PhoenixWebApplicationFactory>
{
    [Fact]
    public async Task GetAllHealth_ShouldSucceded_WhenHealthIsEmpty()
    {
        await thisFactory.ResetDatabase();

        //Arrange
        List<HealthEntity> entities = [
            new HealthEntity()
            {
                Name = "Name1"
            },
            new HealthEntity()
            {
                Name = "Name2"
            }
        ];
        using(IServiceScope db_scope = thisFactory.Services.CreateScope())
        {
            PhoenixDbContext db_context = db_scope.ServiceProvider.GetRequiredService<PhoenixDbContext>();
            await db_context.Health.AddRangeAsync(entities, CancellationToken.None);
            await db_context.SaveChangesAsync(CancellationToken.None);
        }
        IEnumerable<HealthDto> dtos = entities.ToDto();

        //Act
        using IServiceScope handler_scope = thisFactory.Services.CreateScope();
        IMediator mediator = handler_scope.ServiceProvider.GetRequiredService<IMediator>();
        Result<IEnumerable<HealthDto>> result = await mediator.Send(new GetAllHealthQuery(), CancellationToken.None);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(dtos);
    }
}