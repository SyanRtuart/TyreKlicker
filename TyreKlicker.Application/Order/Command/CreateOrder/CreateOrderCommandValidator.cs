using FluentValidation;

namespace TyreKlicker.Application.Order.Command.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Registration).NotEmpty();
        }
    }
}