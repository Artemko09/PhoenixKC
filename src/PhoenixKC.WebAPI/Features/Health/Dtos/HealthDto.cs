namespace PhoenixKC.WebAPI.Features.Health.Dtos;

public sealed class HealthDto
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public required string Name { get; set; }
}