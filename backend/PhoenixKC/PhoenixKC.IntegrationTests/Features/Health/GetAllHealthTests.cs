using FluentResults;
using FluentAssertions;
using System.Net.Http.Json;
using PhoenixKC.Infrastructure;
using PhoenixKC.WebAPI.Features.Health;
using PhoenixKC.WebAPI.Features.Health.Dtos;
using Microsoft.Extensions.DependencyInjection;
using PhoenixKC.Infrastructure.Features.Health;

namespace PhoenixKC.IntegrationTests.Features.Health;

public sealed class GetAllHealthTests(PhoenixWebApplicationFactory thisFactory) : IClassFixture<PhoenixWebApplicationFactory>
{
    private HttpClient HttpClient { get; } = thisFactory.CreateClient();

    [Fact]
    public async Task GetAllHealth_ShouldSucceded_WhenHealthIsEmpty()
    {
        //Arrange
        HealthEntity entity = new()
        {
            Name = "TestHealth"
        };
        using(IServiceScope scope = thisFactory.Services.CreateScope())
        {
            PhoenixDbContext db_context = scope.ServiceProvider.GetRequiredService<PhoenixDbContext>();
            await db_context.Health.AddAsync(entity, CancellationToken.None);
            await db_context.SaveChangesAsync(CancellationToken.None);
        }
        HealthDto dto = entity.ToDto();

        //Act
        using HttpResponseMessage response = await HttpClient.GetAsync("/health", CancellationToken.None);

        //Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        Result<HealthDto[]>? result = await response.Content.ReadFromJsonAsync<Result<HealthDto[]>>(CancellationToken.None);
        if(result is null)
        {
            Assert.Fail("Failed to deserialize result of http response");
        }
        result.Value.Should().BeEquivalentTo([dto]);
    }
}