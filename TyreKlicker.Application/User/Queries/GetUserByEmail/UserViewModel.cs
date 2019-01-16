using System;
using System.Linq.Expressions;

namespace TyreKlicker.Application.User.Queries.GetUserByEmail
{
    public class UserViewModel
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public static Expression<Func<Domain.Entities.User, UserViewModel>> Projection
        {
            get
            {
                return u => new UserViewModel
                {
                    Email = u.Email,
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    PhoneNumber = u.PhoneNumber
                    //firstname lastname phonenumber
                };
            }
        }

        public static UserViewModel Create(Domain.Entities.User user)
        {
            return Projection.Compile().Invoke(user);
        }
    }
}