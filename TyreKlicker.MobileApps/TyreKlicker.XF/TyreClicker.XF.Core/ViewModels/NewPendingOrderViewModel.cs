﻿using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TyreKlicker.XF.Core.Helpers;
using TyreKlicker.XF.Core.Models.Address;
using TyreKlicker.XF.Core.Models.Order;
using TyreKlicker.XF.Core.Services.Address;
using TyreKlicker.XF.Core.Services.Order;
using TyreKlicker.XF.Core.Validators;

namespace TyreKlicker.XF.Core.ViewModels
{
    public class NewPendingOrderViewModel : MvxNavigationViewModel
    {
        private readonly IOrderService _orderService;
        private readonly IAddressService _addressService;

        private CreateNewPendingOrderCommand _order;
        private Address _address;
        private ObservableCollection<Availability> _availability;
        private ValidatableObject<string> _registration;

        public NewPendingOrderViewModel(IMvxLogProvider logProvider,
            IMvxNavigationService navigationService,
            IOrderService orderService, IAddressService addressService) :
            base(logProvider, navigationService)
        {
            _orderService = orderService;
            _addressService = addressService;

            _address = new Address();
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

        public Address Address
        {
            get => _address;
            set
            {
                _address = value;
                RaisePropertyChanged(() => Address);
            }
        }

        public CreateNewPendingOrderCommand Order
        {
            get => _order;
            set
            {
                _order = value;
                RaisePropertyChanged(() => Order);
            }
        }

        public ObservableCollection<Availability> Availability
        {
            get { return _availability; }
            set
            {
                _availability = value;
                RaisePropertyChanged();
            }
        }

        public IMvxAsyncCommand SelectTyreCommand => new MvxAsyncCommand(async () => await NavigateToSelectTyrePageAsync());

        public IMvxAsyncCommand SelectAddressCommand => new MvxAsyncCommand(async () => await NavigateToSelectAddressPageAsync());

        public IMvxAsyncCommand SelectAvailabilityCommand => new MvxAsyncCommand(async () => await NavigateToSelectSelectAvailabilityAsync());

        public IMvxCommand ValidateRegistrationCommand => new MvxCommand(() => ValidateRegistration());

        public IMvxAsyncCommand SubmitOrderCommand => new MvxAsyncCommand(async () => await SubmitOrderAsync());

        public override async Task Initialize()
        {
            await base.Initialize();
            try
            {
                Address = await _addressService.GetPrimaryAddressAsync(Settings.AccessToken,
                    GlobalSetting.Instance.CurrentLoggedInUserId);
            }
            catch
            {
            }
        }

        private async Task SubmitOrderAsync()
        {
            IsBusy = true;
            await _orderService.CreateNewPendingOrder(Settings.AccessToken, _order);
            IsBusy = false;
        }

        private async Task NavigateToSelectTyrePageAsync()
        {
            //ToDo Fix by using an actual object and not the whole order
            Order = await NavigationService.Navigate<SelectVehicalViewModel, CreateNewPendingOrderCommand, CreateNewPendingOrderCommand>(_order);
        }

        private async Task NavigateToSelectSelectAvailabilityAsync()
        {
            Availability = await NavigationService.Navigate<SelectAvailabilityViewModel, ObservableCollection<Availability>, ObservableCollection<Availability>>(new ObservableCollection<Availability>());
        }

        private async Task NavigateToSelectAddressPageAsync()
        {
            Address = await NavigationService.Navigate<SelectAddressViewModel, Address, Address>(new Address());
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