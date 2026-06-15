using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using MyApp.CareerAdvancement.Application.Features.SessionsManagement.UpdateSession;
using MyApp.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;
namespace MyApp.CareerAdvancement.Endpoints.Endpoints.SessionsManagement
{
    internal static class UpdateSessionEndpoint
    {
        // Update uses separate Request DTO (Facet-mapped) + route id param
        public static async Task<IResult> Update(
            IMediator mediator,
            [FromBody] UpdateSessionRequest request,
            int id,
            CancellationToken cancellationToken)
        {
            UpdateSessionCommand command = request.ToCommand(id);
            Result<int> result = await mediator.Send(command, cancellationToken);
            return result.ToApiResult();
        }
    }
}
