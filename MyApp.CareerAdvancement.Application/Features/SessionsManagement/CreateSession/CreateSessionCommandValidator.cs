using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.CareerAdvancement.Application.Features.SessionsManagement.CreateSession
{
    public sealed class CreateSessionCommandValidator : AbstractValidator<CreateSessionCommand>
    {
        public CreateSessionCommandValidator()
        {
            RuleFor(x => x.SessionName).NotEmpty().MaximumLength(255);
            RuleFor(x => x.SessionType).NotEmpty().MaximumLength(10);
            RuleFor(x => x.AppStatus).NotEmpty().MaximumLength(10);
            // Cross-field validation
            RuleFor(x => x.StartDateTime)
                .NotEmpty()
                .LessThan(x => x.EndDateTime)
                .WithMessage("StartDateTime must be before EndDateTime");
            RuleFor(x => x.SessionFrom)
                .NotEmpty()
                .LessThan(x => x.SessionTo)
                .WithMessage("SessionFrom must be before SessionTo");
        }
    }
}
