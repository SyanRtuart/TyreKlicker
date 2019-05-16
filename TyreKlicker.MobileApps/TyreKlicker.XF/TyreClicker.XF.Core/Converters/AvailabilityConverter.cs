using System;
using System.Collections.Generic;
using System.Globalization;
using TyreKlicker.XF.Core.Models.Order;
using Xamarin.Forms;

namespace TyreKlicker.XF.Core.Converters
{
    internal class AvailabilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IEnumerable<Availability>)
            {
                var sortedList = new List<Availability>();

                var availability = (IEnumerable<Availability>)value;

                foreach (var day in availability)
                {
                    bool availabilityAdjusted = false;
                    foreach (var sortedAvailability in sortedList)
                    {
                        availabilityAdjusted = false;
                        if (sortedAvailability.Finish == day.Start)
                        {
                            sortedAvailability.Finish = day.Finish;
                            availabilityAdjusted = true;
                        }
                    }
                    if (!availabilityAdjusted) sortedList.Add(day);
                }

                return sortedList;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}