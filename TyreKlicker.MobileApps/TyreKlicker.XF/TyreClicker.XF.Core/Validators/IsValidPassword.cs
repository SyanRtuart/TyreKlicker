using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TyreKlicker.XF.Core.Validators
{
    public class IsValidPassword<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }
        public bool Check(T value)
        {
            if (value == null) return false;

            var password = value as string;

            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,100}$");

            return regex.IsMatch(password);
        }
    }
}
