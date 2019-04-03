using FluentValidation;
using TyreKlicker.Application.Infrastructure;

namespace TyreKlicker.Application.Address.Command.SetPrimaryAddress
{
    public class SetPrimaryAddressValidator : AbstractValidator<SetPrimaryAddressCommand>
    {
        public SetPrimaryAddressValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty()
                .SetValidator(new GuidValidator());
            RuleFor(x => x.IsPrimary)
                .NotEmpty()
                .NotNull();
        }
    }
}