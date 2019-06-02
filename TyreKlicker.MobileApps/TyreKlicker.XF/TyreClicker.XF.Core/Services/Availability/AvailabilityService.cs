using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TyreKlicker.XF.Core.Models.Order;

namespace TyreKlicker.XF.Core.Services.Availability
{
    public class AvailabilityService : IAvailabilityService
    {
        public ObservableCollection<Models.Order.Availability> GetNext2Weeks()
        {
            var availabilities = new ObservableCollection<Models.Order.Availability>();

            for (var i = 0; i < 14; i++)
            {
                var availability = new Models.Order.Availability
                {
                    Start = DateTime.Now.AddDays(i),
                    TimeSlots = GetDefaultTimeSlots(DateTime.Now.AddDays(i))
                };
                availabilities.Add(availability);
            }

            return availabilities;
        }

        private IEnumerable<TimeSlot> GetDefaultTimeSlots(DateTime startDate)
        {
            return new List<TimeSlot>
            {
                new TimeSlot(startDate, 8),
                new TimeSlot(startDate, 10),
                new TimeSlot(startDate, 12),
                new TimeSlot(startDate, 14),
                new TimeSlot(startDate, 16)
            };
        }
    }
}