using TyreKlicker.XF.Core.Models.Address;

namespace TyreKlicker.XF.Core.Validators
{
    public class IsValidAddressRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null) return false;

            var address = value as Address;

            return !string.IsNullOrEmpty(address?.Postcode);
        }
    }
}