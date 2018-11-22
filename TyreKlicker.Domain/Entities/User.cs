using System;
using System.Collections.Generic;

namespace TyreKlicker.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }


        public virtual IEnumerable<Order> OrdersCreated { get; set; }
        public virtual IEnumerable<Order> OrdersAccepted { get; set; }

    }
}