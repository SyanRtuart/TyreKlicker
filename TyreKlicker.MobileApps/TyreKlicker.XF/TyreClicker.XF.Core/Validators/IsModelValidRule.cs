using TyreKlicker.XF.Core.Models.Tyre;

namespace TyreKlicker.XF.Core.Validators
{
    public class IsModelValidRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var model = value as Model;

            return model != null && !string.IsNullOrWhiteSpace(model.Name);
        }
    }
}