using Ardalis.Result;
using Mediator;
using MyApp.CareerAdvancement.Application.Features.SessionsManagement.GetSessions;
using MyApp.CareerAdvancement.Core.Dtos;
using MyApp.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace MyApp.CareerAdvancement.Endpoints.Endpoints.SessionsManagement
{
    public sealed class GetSessions
    {
        public static async Task<IResult> GetAll(
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            GetSessionsQuery query = new();
            Result<IEnumerable<SessionDto>> result = await mediator.Send(query, cancellationToken);
            return result.ToApiResult();
        }
    }
}
