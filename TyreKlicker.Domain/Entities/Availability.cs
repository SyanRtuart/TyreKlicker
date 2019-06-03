using System;

namespace TyreKlicker.Domain.Entities
{
    public class Availability : Entity
    {
        public DateTime Start { get; set; }

        public DateTime Finish { get; set; }
    }
}