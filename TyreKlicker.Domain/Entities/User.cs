using System.Collections.Generic;

namespace TyreKlicker.Domain.Entities
{
    public class User : Entity
    {
        public User()
        {
            OrdersCreated = new HashSet<Order>();
            OrdersAccepted = new HashSet<Order>();
            Addresses = new HashSet<Address>();
        }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public IEnumerable<Order> OrdersCreated { get; set; }
        public IEnumerable<Order> OrdersAccepted { get; set; }
        public IEnumerable<Address> Addresses { get; set; }
    }
}