using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace MyApp.Endpoints.Abstractions
{
    public static class EndpointsBuilderExtensions
    {
        public static IServiceCollection AddApiEndpointsFromAssemblyOf<T>(this IServiceCollection services)
        {
            services.Scan(x => x.FromAssemblyOf<T>()
            .AddClasses(f => f.AssignableTo<IApiEndpoint>(), false)
            .AsImplementedInterfaces()
            .WithSingletonLifetime());
            return services;
        }

        public static void MapApiEndpoints(this WebApplication app, string baseapi)
        {
            IEndpointRouteBuilder routeBuilder = app.MapGroup(baseapi);
            foreach (IApiEndpoint ep in app.Services.GetServices<IApiEndpoint>())
                ep.Configure(routeBuilder);
        }
    }
}
