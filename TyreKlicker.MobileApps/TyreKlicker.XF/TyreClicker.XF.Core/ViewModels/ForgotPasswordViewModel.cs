using System;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using TyreKlicker.XF.Core.Exceptions;
using TyreKlicker.XF.Core.Helpers;
using TyreKlicker.XF.Core.Models.Account;
using TyreKlicker.XF.Core.Services.Account;
using TyreKlicker.XF.Core.Services.Messages;
using TyreKlicker.XF.Core.Validators;
using Xamarin.Forms;

namespace TyreKlicker.XF.Core.ViewModels
{
    public class ForgotPasswordViewModel : MvxNavigationViewModel
    {
        private readonly IAccountService _accountService;

        private ValidatableObject<string> _currentPassword;
        private ValidatableObject<string> _newPassword;
        private ValidatableObject<string> _confirmNewPassword;
        private readonly MustMatchRule<string> _passwordsMustMatchRule;

        public ForgotPasswordViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IAccountService accountService) : base(logProvider, navigationService)
        {
            _accountService = accountService;
            _currentPassword = new ValidatableObject<string>();
            _newPassword = new ValidatableObject<string>();
            _confirmNewPassword = new ValidatableObject<string>();
            _passwordsMustMatchRule = new MustMatchRule<string>();

            _currentPassword.Value = "Invasi0n!";
            //_currentPassword.Value = "Aa8837229";

            AddValidations();
        }

        
        public ValidatableObject<string> CurrentPassword
        {
            get { return _currentPassword; }
            set
            {
                _currentPassword = value;
                RaisePropertyChanged();
            }
        }

        public ValidatableObject<string> NewPassword
        {
            get { return _newPassword; }
            set
            {
                _newPassword = value;
                RaisePropertyChanged();

            }
        }

        public ValidatableObject<string> ConfirmNewPassword
        {
            get { return _confirmNewPassword; }
            set
            {
                _confirmNewPassword = value;
                RaisePropertyChanged();

            }
        }

        public IMvxCommand SaveChangesAsyncCommand => new MvxCommand(() => SaveChangesAsync());

        public IMvxCommand ValidateCurrentPasswordCommand => new MvxCommand(() => ValidateCurrentPassword());

        public IMvxCommand ValidateNewPasswordCommand => new MvxCommand(() => ValidateNewPassword());

        public IMvxCommand ValidatePasswordsMatchCommand => new MvxCommand(() => ValidatePasswordsMatch());

        private async void SaveChangesAsync()
        {
            if (Validate())
            {
                var command = new ChangePasswordCommand
                {
                    CurrentPassword = _currentPassword.Value,
                    NewPassword = _newPassword.Value,
                    Email = Settings.LastUsedEmail
                };

                try
                {
                    await _accountService.ChangePassword(GlobalSetting.Instance.AuthToken, command);
                    await NavigationService.Close(this);
                    DependencyService.Get<IMessage>().LongAlert("Password successfully updated.", System.Drawing.Color.Empty);
                }
                catch (HttpResponseEx ex)
                {
                    DependencyService.Get<IMessage>().LongAlert(ex.Scenario, Color.Red);
                }
            }
        }

        private bool Validate()
        {
            var isCurrentPasswordValid = ValidateCurrentPassword();
            var isNewPasswordValid = ValidateNewPassword();

            return isNewPasswordValid && isCurrentPasswordValid;
        }

        private bool ValidateCurrentPassword()
        {
            return _currentPassword.Validate();
        }

        private bool ValidateNewPassword()
        {
            return _newPassword.Validate();
        }

        private bool ValidatePasswordsMatch()
        {
            _passwordsMustMatchRule.MustMatchWith = _newPassword.Value;
            return _confirmNewPassword.Validate();
        }

        private void AddValidations()
        {
            _newPassword.Validations.Add(new IsValidPassword<string>(){ValidationMessage = "Password should contain at least 1 uppercase character, lowercase character, non alphanumeric and a number."});
            _confirmNewPassword.Validations.Add(_passwordsMustMatchRule);
        }

    }
}
