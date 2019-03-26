using TyreKlicker.XF.Core.Models.Tyre;

namespace TyreKlicker.XF.Core.Validators
{
    public class IsMakeValidRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var make = value as Make;

            return make != null && !string.IsNullOrWhiteSpace(make.Name);
        }
    }
}