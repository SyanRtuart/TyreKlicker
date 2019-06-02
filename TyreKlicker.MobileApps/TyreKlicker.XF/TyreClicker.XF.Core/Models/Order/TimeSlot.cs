using System;

namespace TyreKlicker.XF.Core.Models.Order
{
    public class TimeSlot
    {
        public TimeSlot(DateTime date, int startHour)
        {
            Start = new DateTime(date.Year, date.Month, date.Day, startHour, 0, 0);
            Finish = new DateTime(date.Year, date.Month, date.Day, startHour + 2, 0, 0);
        }

        public string TimeSlotString => $"{Start:HH:mm} - {Finish:HH:mm}";

        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public bool IsSelected { get; set; } = false;
    }
}