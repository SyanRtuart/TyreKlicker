using System;
using System.Globalization;
using TyreKlicker.XF.Core.Models.Order;
using Xamarin.Forms;

namespace TyreKlicker.XF.Core.Converters
{
    public class AvailabilityToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Availability)
            {
                var availability = (Availability) value;

                return
                    $"{availability.Start:HH:mm} - {availability.Finish:HH:mm} on {availability.Start:dddd, dd MMMM yyyy} ";
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}