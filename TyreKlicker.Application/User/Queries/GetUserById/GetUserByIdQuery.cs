using MediatR;
using System;

namespace TyreKlicker.Application.User.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<UserViewModel>
    {
        public Guid Id { get; set; }
    }
}