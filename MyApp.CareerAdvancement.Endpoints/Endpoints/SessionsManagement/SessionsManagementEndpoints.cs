using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MyApp.Endpoints.Abstractions;
using Microsoft.AspNetCore.Builder;

namespace MyApp.CareerAdvancement.Endpoints.Endpoints.SessionsManagement
{
    internal sealed class SessionsManagementEndpoints : IApiEndpoint
    {
        public void Configure(IEndpointRouteBuilder builder)
        {
            IEndpointRouteBuilder group = builder.MapSessionsManagementGroup();

            group.MapGet("", GetSessions.GetAll)
                .WithTags("SessionsManagement").WithName("GetSessions")
                .ProducesValidationProblem();

            group.MapPost("", CreateSession.Create)
                .WithTags("SessionsManagement").WithName("CreateSession")
                .ProducesValidationProblem();

            group.MapPut("{id:int}", UpdateSessionEndpoint.Update)
                .WithTags("SessionsManagement").WithName("UpdateSession")
                .Produces<int>()
                .Produces(StatusCodes.Status404NotFound);
        }
    }
}
