using FluentValidation;

namespace TyreKlicker.Application.Job.Command.CreateJob
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