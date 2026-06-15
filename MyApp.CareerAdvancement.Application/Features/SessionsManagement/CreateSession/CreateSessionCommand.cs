using Ardalis.Result;
using Mediator;
using MyApp.CareerAdvancement.Core.Entities.SessionsManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.CareerAdvancement.Application.Features.SessionsManagement.CreateSession
{
    public sealed record CreateSessionCommand : ICommand<Result<int>>
    {
        public string SessionName { get; init; } = null!;
        public string SessionType { get; init; } = null!;
        public DateTime StartDateTime { get; init; }
        public DateTime EndDateTime { get; init; }
        public string AppStatus { get; init; } = null!;
        public DateTime SessionFrom { get; init; }
        public DateTime SessionTo { get; init; }
        // Factory method on the command — keeps handler clean
        public AssessmentSession ToAssessmentSession() => new()
        {
            SessionName = SessionName,
            SessionType = SessionType,
            StartDateTime = StartDateTime,
            EndDateTime = EndDateTime,
            AppStatus = AppStatus,
            SessionFrom = SessionFrom,
            SessionTo = SessionTo,
            IsActive = true
        };
    }
}
