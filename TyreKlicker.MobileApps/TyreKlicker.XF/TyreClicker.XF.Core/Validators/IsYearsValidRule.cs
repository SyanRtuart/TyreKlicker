using TyreKlicker.XF.Core.Models.Tyre;

namespace TyreKlicker.XF.Core.Validators
{
    public class IsYearsValidRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var years = value as Years;

            return years != null && !string.IsNullOrWhiteSpace(years.Name);
        }
    }
}