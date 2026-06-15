using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyApp.CareerAdvancement.Core.Data;
using MyApp.CareerAdvancement.Core.Entities.SessionsManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.CareerAdvancement.Application.Features.SessionsManagement.UpdateSession
{
    internal sealed class UpdateSessionCommandHandler(
      CareerCommandDbContext commandDbContext,
      ILogger<UpdateSessionCommandHandler> logger)
      : ICommandHandler<UpdateSessionCommand, Result<int>>
    {
        public async ValueTask<Result<int>> Handle(
            UpdateSessionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                AssessmentSession? session = await commandDbContext.AssessmentSessions
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
                if (session is null)
                    return Result.NotFound("Session with ID not found.");
                request.ApplyTo(ref session);
                await commandDbContext.SaveChangesAsync(cancellationToken);
                return Result.Success(session.Id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error updating session {@Request}", request);
                return Result.Error("Error while updating session.");
            }
        }
    }
}
