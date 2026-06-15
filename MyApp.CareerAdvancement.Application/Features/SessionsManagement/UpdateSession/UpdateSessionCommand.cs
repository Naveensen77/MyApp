using Ardalis.Result;
using Mediator;
using MyApp.CareerAdvancement.Core.Entities.SessionsManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.CareerAdvancement.Application.Features.SessionsManagement.UpdateSession
{
    public sealed class UpdateSessionCommand : ICommand<Result<int>>
    {
        internal int Id { get; set; }   // set by endpoint, not by caller
        public string SessionName { get; init; } = null!;
        public string SessionType { get; init; } = null!;
        public DateTime StartDateTime { get; init; }
        public DateTime EndDateTime { get; init; }
        public string AppStatus { get; init; } = null!;
        public DateTime SessionFrom { get; init; }
        public DateTime SessionTo { get; init; }
        // Mutates tracked entity — handler stays clean
        public void ApplyTo(ref AssessmentSession session)
        {
            session.SessionName = SessionName;
            session.SessionType = SessionType;
            session.StartDateTime = StartDateTime;
            session.EndDateTime = EndDateTime;
            session.AppStatus = AppStatus;
            session.SessionFrom = SessionFrom;
            session.SessionTo = SessionTo;
        }
    }
}
