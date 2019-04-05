using System;

namespace TyreKlicker.Domain.Entities
{
    public class Address : Entity
    {
        public Guid UserId { get; set; }

        public string Street { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        public string PhoneNumber { get; set; }
        public bool PrimaryAddress { get; set; }

        public User User { get; set; }
    }
}