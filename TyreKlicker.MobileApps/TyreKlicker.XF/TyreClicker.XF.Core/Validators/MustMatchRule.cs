﻿namespace TyreKlicker.XF.Core.Validators
{
    public class MustMatchRule<T> : IValidationRule<T>
    {
        public MustMatchRule(ValidatableObject<string> mustMatchWith)
        {
            MustMatch = mustMatchWith.Value;
        }

        public string MustMatch { get; set; }

        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null) return false;

            var str = value as string;
            return str == MustMatch;
        }
    }
}