using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
namespace MyApp.CareerAdvancement.Endpoints.Endpoints
{
    public static class SessionsManagementGroupExtensions
    {
        public static IEndpointRouteBuilder MapSessionsManagementGroup(
            this IEndpointRouteBuilder builder)
        {
            ArgumentNullException.ThrowIfNull(builder);
            return builder.MapGroup("career/sessions")
                .WithDisplayName("Sessions Management")
                .WithTags("SessionsManagement");
        }
    }
}
