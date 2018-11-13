using System;
using System.Linq.Expressions;
using TyreKlicker.Domain.Entities;

namespace TyreKlicker.Application.User.Queries.GetUserByAccountId
{
    public class UserViewModel
    {
        public Guid UserId { get; set; }

        public Guid AccountId { get; set; }

        public static Expression<Func<Domain.Entities.User, UserViewModel>> Projection
        {
            get
            {
                return u => new UserViewModel
                {
                    //AccountId = u.AccountId,
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