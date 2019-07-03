using System;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using TyreKlicker.XF.Core.Helpers;
using TyreKlicker.XF.Core.Models.Payment;
using TyreKlicker.XF.Core.Services.Payment;
using TyreKlicker.XF.Core.Services.Stripe;

namespace TyreKlicker.XF.Core.ViewModels
{
    public class AccountViewModel : MvxNavigationViewModel
    {
        private readonly IPaymentService _paymentService;
        private readonly IStripeService _stripeService;

        private string _creditCardNumber;
        private string _cvc;
        private DateTime _expiryDate;

        public AccountViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService,
            IPaymentService paymentService, IStripeService stripeService) : base(
            logProvider, navigationService)
        {
            _paymentService = paymentService;
            _stripeService = stripeService;

            CreditCardNumber = "4000000000000002";
            Cvc = "123";
            ExpiryDate = DateTime.Now + TimeSpan.FromDays(500);
        }

        public string CreditCardNumber
        {
            get => _creditCardNumber;
            set
            {
                _creditCardNumber = value;
                RaisePropertyChanged();
            }
        }

        public DateTime ExpiryDate
        {
            get => _expiryDate;
            set
            {
                _expiryDate = value;
                RaisePropertyChanged();
            }
        }


        public string Cvc
        {
            get => _cvc;
            set
            {
                _cvc = value;
                RaisePropertyChanged();
            }
        }

        public IMvxCommand LogoutCommand => new MvxCommand(async () => await LogoutAsync());

        public IMvxCommand MakePaymentCommand => new MvxCommand(async () => await MakePaymentAsync());

        public IMvxCommand ChangePasswordCommand => new MvxCommand(async () => await ChangePassword());

        private async Task LogoutAsync()
        {
            Settings.AccessToken = string.Empty;
            await NavigationService.Navigate<LoginViewModel>();
        }

        private async Task MakePaymentAsync()
        {
            var stripeToken = _stripeService.CreateToken(_creditCardNumber, _expiryDate.Month, _expiryDate.Year, _cvc);

            var payment = new Payment
            {
                Amount = 500,
                Currency = "usd",
                Description = "bla",
                Email = "rs@yahoo.com",
                Token = stripeToken
            };

            await _paymentService.MakePayment(payment, GlobalSetting.StripeSecretKey);
        }

        private async Task ChangePassword()
        {
            await NavigationService.Navigate<ForgotPasswordViewModel>();
        }

    }
}