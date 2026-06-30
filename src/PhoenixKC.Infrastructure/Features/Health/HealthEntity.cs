namespace PhoenixKC.Infrastructure.Features.Health;

/// <remarks>
/// Configuration of the entity located in <see cref="HealthConfiguration"/>
/// </remarks>
public sealed class HealthEntity
{
    //Value properties
    public Guid Id { get; set; }
    public required string Name { get; set; }
}