using System.Collections;
using MvvmCross.Binding.Extensions;

namespace TyreKlicker.XF.Core.Validators
{
    internal class IsCountNotZeroRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null) return false;

            var enumerable = value as IEnumerable;

            return enumerable.Count() > 0;
        }
    }
}