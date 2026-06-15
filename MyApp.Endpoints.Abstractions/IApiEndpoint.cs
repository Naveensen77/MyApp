using Microsoft.AspNetCore.Routing;


namespace MyApp.Endpoints.Abstractions
{
    public interface IApiEndpoint
    {
        void Configure(IEndpointRouteBuilder builder);
    }
}
