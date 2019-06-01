using System;
using System.Collections.Generic;

namespace TyreKlicker.Domain.Entities
{
    public class Order : Entity
    {
        public Guid? AcceptedByUserId { get; set; }

        public Guid AddressId { get; set; }

        public string Registration { get; set; }

        public string Description { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string Year { get; set; }

        public string Trim { get; set; }

        public string Tyre { get; set; }

        public IEnumerable<Availability> Availability { get; set; }

        public bool Complete { get; set; }

        public User CreatedByUser { get; set; }
        public User AcceptedByUser { get; set; }
        public Address Address { get; set; }
    }
}