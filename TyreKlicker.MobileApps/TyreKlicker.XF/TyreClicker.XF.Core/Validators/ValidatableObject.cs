using System.Collections.Generic;
using System.Linq;
using TyreKlicker.XF.Core.ViewModels.Base;

namespace TyreKlicker.XF.Core.Validators
{
    public class ValidatableObject<T> : ExtendedBindableObject, IValidity
    {
        private List<string> _errors;
        private bool _isValid;
        private T _value;

        public ValidatableObject()
        {
            _isValid = true;
            _errors = new List<string>();
            Validations = new List<IValidationRule<T>>();
        }

        public List<IValidationRule<T>> Validations { get; }

        public List<string> Errors
        {
            get => _errors;
            set
            {
                _errors = value;
                RaisePropertyChanged(() => Errors);
            }
        }

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                RaisePropertyChanged(() => Value);
            }
        }

        public bool IsValid
        {
            get => _isValid;
            set
            {
                _isValid = value;
                RaisePropertyChanged(() => IsValid);
            }
        }

        public bool Validate()
        {
            Errors.Clear();

            var errors = Validations.Where(v => !v.Check(Value))
                .Select(v => v.ValidationMessage);

            Errors = errors.ToList();
            IsValid = !Errors.Any();

            return IsValid;
        }
    }
}