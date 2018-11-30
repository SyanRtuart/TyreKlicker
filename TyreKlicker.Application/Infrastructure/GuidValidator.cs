using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

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
