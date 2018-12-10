using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using TyreKlicker.Application.Infrastructure;


namespace TyreKlicker.Application.Order.Command.CompleteOrder
{
    public class CompleteOrderCommandValidator : AbstractValidator<CompleteOrderCommand>
    {
        public CompleteOrderCommandValidator()
        {
            RuleFor(o => o.OrderId)
                .NotNull()
                .NotEmpty()
                .SetValidator(new GuidValidator());

            RuleFor(o => o.Complete)
                .NotEmpty()
                .NotNull();


        }
    }
}
