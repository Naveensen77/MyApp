using Ardalis.Result;
using Mediator;
using MyApp.CareerAdvancement.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.CareerAdvancement.Application.Features.SessionsManagement.GetSessions
{
    public sealed record GetSessionsQuery : IQuery<Result<IEnumerable<SessionDto>>>;

}
