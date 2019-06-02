using TyreKlicker.XF.Core.Models.Tyre;

namespace TyreKlicker.XF.Core.Validators
{
    public class IsWheelValidRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null) return false;

            var wheel = value as Wheel;

            return wheel != null && !string.IsNullOrWhiteSpace(wheel.Tire);
        }
    }
}