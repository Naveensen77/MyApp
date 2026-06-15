using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyApp.CareerAdvancement.Core.Data;
using MyApp.CareerAdvancement.Core.Dtos;

namespace MyApp.CareerAdvancement.Application.Features.SessionsManagement.GetSessions
{
    internal sealed class GetSessionsQueryHandler(
     CareerQueryDbContext queryDbContext,
     ILogger<GetSessionsQueryHandler> logger)
     : IQueryHandler<GetSessionsQuery, Result<IEnumerable<SessionDto>>>
    {
        public async ValueTask<Result<IEnumerable<SessionDto>>> Handle(
            GetSessionsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<SessionDto> dtos = await queryDbContext.AssessmentSessions
                    .Select(SessionDto.Projection)   // uses the static Expression — server-side SQL
                    .ToListAsync(cancellationToken);
                logger.LogInformation("Retrieved {Count} sessions", dtos.Count);
                return Result.Success<IEnumerable<SessionDto>>(dtos);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving sessions");
                return Result.Error("Error while retrieving sessions.");
            }
        }
    }
}
