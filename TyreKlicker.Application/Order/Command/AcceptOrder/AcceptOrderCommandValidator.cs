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
            RuleFor(o => o.UserId)
                .NotNull()
                .NotEmpty()
                .SetValidator(new GuidValidator());
            RuleFor(o => o.OrderId)
                .NotNull()
                .NotEmpty()
                .SetValidator(new GuidValidator());

            //ToDo Validate that User actually exists
        }

    }
}
