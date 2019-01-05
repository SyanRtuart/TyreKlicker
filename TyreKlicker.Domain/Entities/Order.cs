using System;

namespace TyreKlicker.Domain.Entities
{
    public class Order : Entity
    {
        public Guid CreatedByUserId { get; set; }

        public Guid? AcceptedByUserId { get; set; }

        public string Registration { get; set; }

        public string Description { get; set; }

        public bool Complete { get; set; }

        public User CreatedByUser { get; set; }
        public User AcceptedByUser { get; set; }
    }
}