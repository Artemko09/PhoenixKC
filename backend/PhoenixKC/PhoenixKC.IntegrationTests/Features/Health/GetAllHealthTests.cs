using FluentResults;
using FluentAssertions;
using System.Net.Http.Json;
using PhoenixKC.Infrastructure;
using PhoenixKC.WebAPI.Features.Health;
using PhoenixKC.WebAPI.Features.Health.Dtos;
using Microsoft.Extensions.DependencyInjection;
using PhoenixKC.Infrastructure.Features.Health;

namespace PhoenixKC.IntegrationTests.Features.Health;

public sealed class GetAllHealthTests(PhoenixWebApplicationFactory factory) : IClassFixture<PhoenixWebApplicationFactory>
{
    private HttpClient HttpClient { get; } = factory.CreateClient();
    private PhoenixDbContext DbContext { get; } = factory.Server.Services.GetRequiredService<PhoenixDbContext>();

    [Fact]
    public async Task GetAllHealth_ShouldSucceded_WhenHealthIsEmpty()
    {
        //Arrange
        HealthEntity entity = new()
        {
            Name = "TestHealth"
        };
        await DbContext.Health.AddAsync(entity, CancellationToken.None);
        await DbContext.SaveChangesAsync(CancellationToken.None);
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