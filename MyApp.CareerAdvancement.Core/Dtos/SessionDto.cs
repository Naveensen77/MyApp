using MyApp.CareerAdvancement.Core.Entities.SessionsManagement;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MyApp.CareerAdvancement.Core.Dtos
{
    public sealed record SessionDto(
     int Id,
     string SessionName,
     string SessionType,
     DateTime StartDateTime,
     DateTime EndDateTime,
     string AppStatus,
     bool IsActive,
     DateTime CreatedOn,
     string CreatedBy)
    {
        // Static Expression used in .Select() — avoids client-side evaluation
        public static Expression<Func<AssessmentSession, SessionDto>> Projection => session =>
            new SessionDto(
                session.Id,
                session.SessionName,
                session.SessionType,
                session.StartDateTime,
                session.EndDateTime,
                session.AppStatus,
                session.IsActive,
                session.CreatedOn,
                session.CreatedBy);
    }
}
