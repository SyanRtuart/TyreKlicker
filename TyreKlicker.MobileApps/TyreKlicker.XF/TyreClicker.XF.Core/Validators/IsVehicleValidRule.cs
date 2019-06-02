using TyreKlicker.XF.Core.Models.Order;

namespace TyreKlicker.XF.Core.Validators
{
    public class IsVehicleValidRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null) return false;

            var vehicle = value as Vehicle;

            return vehicle != null && !string.IsNullOrWhiteSpace(vehicle.Trim);
        }
    }
}