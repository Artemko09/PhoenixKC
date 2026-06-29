namespace PhoenixKC.WebAPI.Features;

public interface IEndpointsProvider
{
    public abstract IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder builder);
}