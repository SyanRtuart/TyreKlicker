using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TyreKlicker.Application.User.Command.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.FirstName).MinimumLength(2).MaximumLength(30);
            RuleFor(x => x.LastName).MinimumLength(2).MaximumLength(30);
            RuleFor(x => x.PhoneNumber).NotEmpty();
        }
    }
}