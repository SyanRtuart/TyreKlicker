﻿using System;
using MediatR;

namespace TyreKlicker.Application.User.Command.CreateUser
{
    public class CreateUserCommand : IRequest
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }
    }
}