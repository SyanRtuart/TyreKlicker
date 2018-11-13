using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace TyreKlicker.Application.User.Queries.GetUserByAccountId
{
    public class GetUserByAccountIdQuery : IRequest<UserViewModel>
    {
        public Guid Id { get; set; }
    }
}