using System;
using System.Linq.Expressions;
using TyreKlicker.Domain.Entities;

namespace TyreKlicker.Application.User.Queries.GetUserByEmail
{
    public class UserViewModel
    {
        public Guid UserId { get; set; }

        public string Email { get; set; }

        public static Expression<Func<Domain.Entities.User, UserViewModel>> Projection
        {
            get
            {
                return u => new UserViewModel
                {
                    Email = u.Email,
                    UserId = u.Id
                };
            }
        }

        public static UserViewModel Create(Domain.Entities.User user)
        {
            return Projection.Compile().Invoke(user);
        }
    }
}