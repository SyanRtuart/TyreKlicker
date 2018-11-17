using System;
using System.Linq.Expressions;

namespace TyreKlicker.Application.User.Queries.GetUserByEmail
{
    public class UserViewModel
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public static Expression<Func<Domain.Entities.User, UserViewModel>> Projection
        {
            get
            {
                return u => new UserViewModel
                {
                    Email = u.Email,
                    Id = u.Id
                };
            }
        }

        public static UserViewModel Create(Domain.Entities.User user)
        {
            return Projection.Compile().Invoke(user);
        }
    }
}