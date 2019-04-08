using System;
using System.Linq.Expressions;

namespace TyreKlicker.Application.Address.Query.GetPrimaryAddress
{
    public class AddressDto
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string Postcode { get; set; }

        public string PhoneNumber { get; set; }

        public bool PrimaryAddress { get; set; }

        public static Expression<Func<Domain.Entities.Address, AddressDto>> Projection
        {
            get
            {
                return a => new AddressDto
                {
                    Id = a.Id,
                    PhoneNumber = a.PhoneNumber,
                    UserId = a.UserId,
                    City = a.City,
                    Postcode = a.Postcode,
                    PrimaryAddress = a.PrimaryAddress,
                    Street = a.Street
                };
            }
        }

        public static AddressDto Create(Domain.Entities.Address address)
        {
            return Projection.Compile().Invoke(address);
        }
    }
}