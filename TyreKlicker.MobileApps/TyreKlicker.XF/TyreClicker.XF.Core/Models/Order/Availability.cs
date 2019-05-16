using System;
using System.Collections.Generic;

namespace TyreKlicker.XF.Core.Models.Order
{
    public class Availability
    {
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public IEnumerable<TimeSlot> TimeSlots { get; set; }
    }
}