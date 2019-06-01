using MvvmCross.Commands;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using System.Threading.Tasks;
using TyreKlicker.XF.Core.Exceptions;
using TyreKlicker.XF.Core.Models.Authentication;
using TyreKlicker.XF.Core.Services.AuthenticationService;
using TyreKlicker.XF.Core.Services.Messages;
using TyreKlicker.XF.Core.Validators;
using Xamarin.Forms;

namespace TyreKlicker.XF.Core.ViewModels
{
    [MvxModalPresentation()]
    public class RegistrationViewModel : MvxNavigationViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IAuthenticationService _authenticationService;

        private ValidatableObject<string> _email;
        private ValidatableObject<string> _password;
        private ValidatableObject<string> _confirmPassword;
        private ValidatableObject<string> _firstName;
        private ValidatableObject<string> _lastName;

        public RegistrationViewModel(IMvxLogProvider logProvider,
            IMvxNavigationService navigationService,
            IAuthenticationService authenticationService)
            : base(logProvider, navigationService)
        {
            _navigationService = navigationService;
            _authenticationService = authenticationService;

            _email = new ValidatableObject<string>();
            _password = new ValidatableObject<string>();
            _confirmPassword = new ValidatableObject<string>();
            _firstName = new ValidatableObject<string>();
            _lastName = new ValidatableObject<string>();

            _email.Value = "ryan@theinternet.com";
            //_password.Value = "Test123!";
            //_confirmPassword.Value = "Test123!";
            _firstName.Value = "Ryan";
            _lastName.Value = "Stuart";

            AddValidations();
        }

        public ValidatableObject<string> Email
        {
            get => _email;
            set
            {
                _email = value;
                RaisePropertyChanged(() => Email);
            }
        }

        public ValidatableObject<string> Password
        {
            get => _password;
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
            }
        }

        public ValidatableObject<string> ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                RaisePropertyChanged(() => ConfirmPassword);
            }
        }

        public ValidatableObject<string> FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                RaisePropertyChanged(() => FirstName);
            }
        }

        public ValidatableObject<string> LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                RaisePropertyChanged(() => LastName);
            }
        }

        public IMvxCommand RegisterCommand => new MvxCommand(async () => await RegisterAsync());

        public IMvxCommand ValidateEmailCommand => new MvxCommand(() => ValidateEmail());

        public IMvxCommand ValidatePasswordCommand => new MvxCommand(() => ValidatePassword());

        public IMvxCommand ValidateConfirmPasswordCommand => new MvxCommand(() => ValidateConfirmPassword());

        public IMvxCommand ValidateFirstNameCommand => new MvxCommand(() => ValidateFirstName());

        public IMvxCommand ValidateLastNameCommand => new MvxCommand(() => ValidateLastName());

        private async Task RegisterAsync()
        {
            if (Validate())
            {
                try
                {
                    var registerRequest = new RegisterRequest
                    {
                        Email = _email.Value,
                        Password = _password.Value,
                        ConfirmPassword = _confirmPassword.Value,
                        LastName = _lastName.Value,
                        FirstName = _firstName.Value
                    };

                    await _authenticationService.Register(registerRequest);
                    DependencyService.Get<IMessage>().LongAlert("Please check your email for conformation.", System.Drawing.Color.Empty);
                    //ToDo Pass back email to login VM
                    await _navigationService.Close(this);
                }
                catch (HttpResponseEx ex)
                {
                    DependencyService.Get<IMessage>().LongAlert(ex.Scenario, System.Drawing.Color.Red);
                }
            }
        }

        private bool Validate()
        {
            var isValidUser = ValidateEmail();
            var isValidPassword = ValidatePassword();
            var isValidConfirmPassword = ValidateConfirmPassword();
            var isValidFirstName = ValidateFirstName();
            var isValidLastName = ValidateLastName();

            return isValidUser && isValidPassword && isValidFirstName && isValidLastName && isValidConfirmPassword;
        }

        private bool ValidateEmail()
        {
            return _email.Validate();
        }

        private bool ValidatePassword()
        {
            return _password.Validate();
        }

        private bool ValidateConfirmPassword()
        {
            return _confirmPassword.Validate();
        }

        private bool ValidateLastName()
        {
            return _lastName.Validate();
        }

        private bool ValidateFirstName()
        {
            return _firstName.Validate();
        }

        private void AddValidations()
        {
            //ToDo add password & email Validators
            _email.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "An email is required." });
            _password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A password is required." });
            // _confirmPassword.Validations.Add(new MustMatchRule<string>(_password) { ValidationMessage = "password must match" });
            _firstName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A first name is required." });
            _lastName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A last name is required." });
        }
    }
}