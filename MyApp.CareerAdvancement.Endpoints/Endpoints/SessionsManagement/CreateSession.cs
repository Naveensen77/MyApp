using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using MyApp.CareerAdvancement.Application.Features.SessionsManagement.CreateSession;
using MyApp.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace MyApp.CareerAdvancement.Endpoints.Endpoints.SessionsManagement
{
    public sealed class CreateSession
    {
        // Command IS the request body here — no separate DTO needed for create
        public static async Task<IResult> Create(
            IMediator mediator,
            [FromBody] CreateSessionCommand command,
            CancellationToken cancellationToken)
        {
            Result<int> result = await mediator.Send(command, cancellationToken);
            return result.ToApiResult();
        }
    }
}
