using MediatR;
using System;

namespace TyreKlicker.Application.User.Command.CreateUser
{
    public class CreateUserCommand : IRequest
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }
    }
}