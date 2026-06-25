namespace PhoenixKC.Infrastructure.Features;

public class EntityBase
{
    //Value properties
    public Guid Id { get; set; } = Guid.CreateVersion7();
}