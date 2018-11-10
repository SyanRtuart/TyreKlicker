using System;

namespace TyreKlicker.Domain.Entities
{
    public class Order
    {
        public Guid OrderId { get; set; }

        public Guid UserId { get; set; }

        public string Registration { get; set; }

        public string Description { get; set; }
    }
}