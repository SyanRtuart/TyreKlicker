using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using TyreKlicker.Application.Infrastructure;

namespace TyreKlicker.Application.Order.Command.AcceptOrder
{
    public class AcceptOrderCommandValidator : AbstractValidator<AcceptOrderCommand>
    {
        public AcceptOrderCommandValidator()
        {
            RuleFor(d => d.UserId)
                .NotNull()
                .NotEmpty()
                .SetValidator(new GuidValidator());
        }

    }
}
