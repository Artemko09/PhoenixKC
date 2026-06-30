using System.Reflection;
using PhoenixKC.WebAPI.Features;

namespace PhoenixKC.WebAPI.Extensions;

public static class DependencyInjectionExtensions
{
    public static void MapEndpointsFromAssembly(this IEndpointRouteBuilder builder, Assembly? assembly = null)
    {
        assembly ??= typeof(IEndpointsProvider).Assembly;
        IEnumerable<Type> types = assembly.GetTypes().Where(type => typeof(IEndpointsProvider).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract);
        foreach(Type type in types)
        {
            var endpoints = (IEndpointsProvider)Activator.CreateInstance(type)!;
            endpoints.MapEndpoints(builder);
        }
    }
}