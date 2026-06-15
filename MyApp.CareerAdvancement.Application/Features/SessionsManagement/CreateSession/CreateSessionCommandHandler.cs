using Ardalis.Result;
using Mediator;
using Microsoft.Extensions.Logging;
using MyApp.CareerAdvancement.Core.Data;
using MyApp.CareerAdvancement.Core.Entities.SessionsManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.CareerAdvancement.Application.Features.SessionsManagement.CreateSession
{
    internal sealed class CreateSessionCommandHandler(
     CareerCommandDbContext commandDbContext,
     ILogger<CreateSessionCommandHandler> logger)
     : ICommandHandler<CreateSessionCommand, Result<int>>
    {
        public async ValueTask<Result<int>> Handle(
            CreateSessionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                AssessmentSession session = request.ToAssessmentSession();
                commandDbContext.AssessmentSessions.Add(session);
                await commandDbContext.SaveChangesAsync(cancellationToken);
                return Result.Success(session.Id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating session {SessionName}", request.SessionName);
                return Result.Error("Error while creating session.");
            }
        }
    }
}
