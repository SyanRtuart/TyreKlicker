using FluentValidation;
using System;

namespace TyreKlicker.Application.Infrastructure
{
    public class GuidValidator : AbstractValidator<Guid>
    {
        public GuidValidator()
        {
            RuleFor(x => x).NotEmpty();
        }
    }
}