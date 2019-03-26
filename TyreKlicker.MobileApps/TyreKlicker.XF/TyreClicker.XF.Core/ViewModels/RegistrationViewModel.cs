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
        private readonly Services.IAppSettings _settings;
        private readonly IAuthenticationService _authenticationService;

        private ValidatableObject<string> _email;
        private ValidatableObject<string> _password;
        private ValidatableObject<string> _confirmPassword;
        private ValidatableObject<string> _firstName;
        private ValidatableObject<string> _lastName;

        public RegistrationViewModel(IMvxLogProvider logProvider,
            IMvxNavigationService navigationService,
            Services.IAppSettings settings,
            IAuthenticationService authenticationService)
            : base(logProvider, navigationService)
        {
            _navigationService = navigationService;
            _settings = settings;
            _authenticationService = authenticationService;

            _email = new ValidatableObject<string>();
            _password = new ValidatableObject<string>();
            _confirmPassword = new ValidatableObject<string>();
            _firstName = new ValidatableObject<string>();
            _lastName = new ValidatableObject<string>();

            _email.Value = "Test@Test.co.uk";
            _password.Value = "Test123!";
            _confirmPassword.Value = "Test123!";
            _firstName.Value = "Test1";
            _lastName.Value = "23232323";

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

        public IMvxAsyncCommand RegisterCommand => new MvxAsyncCommand(async () => await RegisterAsync());

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
                }
                catch (HttpResponseEx ex)
                {
                    //ToDo show errors
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
            var valid = _password.Value == _confirmPassword.Value;
            _confirmPassword.IsValid = valid;
            return valid;
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
            _email.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A username is required." });
            _password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A password is required." });
            _firstName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A first name is required." });
            _lastName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A last name is required." });
        }
    }
}