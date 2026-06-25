using FluentAssertions;

namespace PhoenixKC.IntegrationTests.Features.Health;

public sealed class GetAllHealthTests(PhoenixWebApplicationFactory factory) : IClassFixture<PhoenixWebApplicationFactory>
{
    private HttpClient HttpClient { get; } = factory.CreateClient();

    [Fact]
    public async Task GetAllHealth_ShouldSucceded_WhenHealthIsEmpty()
    {
        //Act
        using HttpResponseMessage response = await HttpClient.GetAsync("/health", CancellationToken.None);

        //Assert
        response.IsSuccessStatusCode.Should().BeFalse();
    }
}