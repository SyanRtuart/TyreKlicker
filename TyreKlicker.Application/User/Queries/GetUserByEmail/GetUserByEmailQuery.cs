﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace TyreKlicker.Application.User.Queries.GetUserByEmail
{
    public class GetUserByEmailQuery : IRequest<UserViewModel>
    {
        public string Email { get; set; }
    }
}