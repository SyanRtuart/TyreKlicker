using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using System.Threading.Tasks;
using TyreKlicker.XF.Core.Helpers;
using TyreKlicker.XF.Core.Models.Order;
using TyreKlicker.XF.Core.Services.Order;
using TyreKlicker.XF.Core.Services.Tyre;
using TyreKlicker.XF.Core.Validators;

namespace TyreKlicker.XF.Core.ViewModels
{
    public class NewPendingOrderViewModel : MvxNavigationViewModel
    {
        private readonly IOrderService _orderService;
        private readonly ITyreService _tyreService;

        private CreateNewPendingOrderCommand _order;

        private ValidatableObject<string> _registration;

        public NewPendingOrderViewModel(IMvxLogProvider logProvider,
            IMvxNavigationService navigationService,
            IOrderService orderService,
            ITyreService tyreService) :
            base(logProvider, navigationService)
        {
            _tyreService = tyreService;
            _orderService = orderService;

            _order = new CreateNewPendingOrderCommand(GlobalSetting.Instance.CurrentLoggedInUserId);
            _registration = new ValidatableObject<string>();

            AddValidations();
        }

        public ValidatableObject<string> Registration
        {
            get { return _registration; }
            set
            {
                _registration = value;
                RaisePropertyChanged(() => Registration);
            }
        }

        public IMvxAsyncCommand SelectTyreCommand => new MvxAsyncCommand(async () => await NavigateToSelectTyrePage());

        public IMvxCommand ValidateRegistrationCommand => new MvxCommand(() => ValidateRegistration());

        public IMvxAsyncCommand SubmitOrderCommand => new MvxAsyncCommand(async () => await SubmitOrderAsync());

        private async Task SubmitOrderAsync()
        {
            await _orderService.CreateNewPendingOrder(Settings.AccessToken, _order);
        }

        private async Task NavigateToSelectTyrePage()
        {
            var result = await NavigationService.Navigate<SelectVehicalViewModel, CreateNewPendingOrderCommand, CreateNewPendingOrderCommand>(_order);
        }

        private bool ValidateRegistration()
        {
            var result = _registration.Validate();

            if (result) _order.Registration = _registration.Value;

            return result;
        }

        private void AddValidations()
        {
            _registration.Validations.Add(new VehicleRegistrationRule<string> { ValidationMessage = "A valid UK vehical registration is required." });
        }
    }
}