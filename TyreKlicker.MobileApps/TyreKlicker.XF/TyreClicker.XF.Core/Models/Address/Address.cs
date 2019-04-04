using System;

namespace TyreKlicker.XF.Core.Models.Address
{
    public class Address
    {
        public Guid UserId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        public string PhoneNumber { get; set; }
        public bool PrimaryAddress { get; set; }
    }
}