using MediatR;

namespace TyreKlicker.Application.User.Queries.GetUserByEmail
{
    public class GetUserByEmailQuery : IRequest<UserViewModel>
    {
        public string Email { get; set; }
    }
}