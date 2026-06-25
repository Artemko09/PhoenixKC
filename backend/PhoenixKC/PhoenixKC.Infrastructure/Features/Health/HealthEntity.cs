namespace PhoenixKC.Infrastructure.Features.Health;

/// <remarks>
/// Configuration of the entity located in <see cref="HealthConfiguration"/>
/// </remarks>
public sealed class HealthEntity : EntityBase
{
    //Value properties
    public required string Name { get; set; }
}