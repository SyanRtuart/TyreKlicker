using TyreKlicker.XF.Core.Models.Tyre;

namespace TyreKlicker.XF.Core.Validators
{
    internal class IsVehicleTrimValidRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null) return false;

            var trim = value as VehicleTrim;

            return trim != null && !string.IsNullOrWhiteSpace(trim.Trim);
        }
    }
}