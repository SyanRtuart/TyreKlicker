using System;

namespace TyreKlicker.Domain.Entities
{
    public class Order
    {
        public Guid OrderId { get; set; }

        public Guid CreatedByUserId { get; set; }

        public Guid AcceptedByUserId { get; set; }

        public string Registration { get; set; }

        public string Description { get; set; }

        public virtual User CreatedByUser { get; set; }
        public virtual User AcceptedByUser { get; set; }

    }
}
