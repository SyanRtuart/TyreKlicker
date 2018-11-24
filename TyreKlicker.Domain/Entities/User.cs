using System;
using System.Collections.Generic;

namespace TyreKlicker.Domain.Entities
{
    public class User
    {
        public User()
        {
            OrdersCreated = new HashSet<Order>();
            OrdersAccepted = new HashSet<Order>();
        }

        public Guid Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public IEnumerable<Order> OrdersCreated { get; set; }
        public IEnumerable<Order> OrdersAccepted { get; set; }
    }
}