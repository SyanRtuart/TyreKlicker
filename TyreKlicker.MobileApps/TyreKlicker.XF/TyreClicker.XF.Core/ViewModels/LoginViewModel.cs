﻿using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using System.Threading.Tasks;
using TyreKlicker.XF.Core.Helpers;
using TyreKlicker.XF.Core.Models.Authentication;
using TyreKlicker.XF.Core.Services.AuthenticationService;
using TyreKlicker.XF.Core.Validators;

namespace TyreKlicker.XF.Core.ViewModels
{
    public class LoginViewModel : MvxNavigationViewModel
    {
        private readonly IAuthenticationService _authenticationService;

        private ValidatableObject<string> _email;
        private ValidatableObject<string> _password;

        public LoginViewModel(IMvxLogProvider logProvider,
            IMvxNavigationService navigationService,
            IAuthenticationService authenticationService)
            : base(logProvider, navigationService)
        {
            _authenticationService = authenticationService;

            NavigateToRegistrationPageCommand = new MvxCommand(async () => await NavigationService.Navigate<RegistrationViewModel>());
            LoginCommand = new MvxCommand(async () => await LoginAsync());

            _email = new ValidatableObject<string>();
            _password = new ValidatableObject<string>();

            _email.Value = "ryan@theinternet.com";
            _password.Value = "Invasi0n!";

            AddValidations();
        }

        public ValidatableObject<string> Email
        {
            get => _email;
            set
            {
                _email = value;
                RaisePropertyChanged(() => Email);
                Settings.LastUsedEmail = value.Value;
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

        public IMvxCommand LoginCommand { get; }

        public IMvxCommand NavigateToRegistrationPageCommand { get; }

        public IMvxCommand ValidateEmailCommand => new MvxCommand(() => ValidateEmail());

        public IMvxCommand ValidatePasswordCommand => new MvxCommand(() => ValidatePassword());

        private async Task LoginAsync()
        {
            if (Validate())
            {
                var loginRequest = new LoginRequest
                {
                    Email = _email.Value,
                    Password = _password.Value
                };

                var token = await _authenticationService.GetTokenAsync(loginRequest);

                if (!string.IsNullOrEmpty(token.AccessToken))
                {
                    Settings.AccessToken = token.AccessToken;
                    await NavigationService.Navigate<SplitRootViewModel>();
                }
                //handle else
            }
        }

        private bool Validate()
        {
            var isValidUser = ValidateEmail();
            var isValidPassword = ValidatePassword();

            return isValidUser && isValidPassword;
        }

        private bool ValidateEmail()
        {
            return _email.Validate();
        }

        private bool ValidatePassword()
        {
            return _password.Validate();
        }

        private void AddValidations()
        {
            _email.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A username is required." });
            _password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A password is required." });
        }
    }
}