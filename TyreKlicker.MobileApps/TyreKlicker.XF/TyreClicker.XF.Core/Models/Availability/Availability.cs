using System;
using System.Collections.Generic;
using System.Text;

namespace TyreKlicker.XF.Core.Models.Availability
{
    public class Availability
    {
        public DateTime Date { get; set; }
        public IEnumerable<TimeSlot> TimeSlots { get; set; }
    }
}
