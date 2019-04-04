using FluentValidation;
using TyreKlicker.Application.Infrastructure;

namespace TyreKlicker.Application.Address.Command.CreateAddress
{
    public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
    {
        public CreateAddressCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotNull()
                .NotEmpty()
                .SetValidator(new GuidValidator());
            RuleFor(x => x.Street)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.Postcode)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.City)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.PhoneNumber)
                .NotNull()
                .NotEmpty();
        }
    }
}