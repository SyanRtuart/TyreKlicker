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
        private ValidatableObject<Address> _address;
        private ValidatableObject<ObservableCollection<Availability>> _availability;
        private ValidatableObject<string> _registration;

        public NewPendingOrderViewModel(IMvxLogProvider logProvider,
            IMvxNavigationService navigationService,
            IOrderService orderService, IAddressService addressService) :
            base(logProvider, navigationService)
        {
            _orderService = orderService;
            _addressService = addressService;

            _address = new ValidatableObject<Address>();
            _order = new CreateNewPendingOrderCommand(GlobalSetting.Instance.CurrentLoggedInUserId);
            _registration = new ValidatableObject<string>();
            _availability = new ValidatableObject<ObservableCollection<Availability>>
            {
                Value = new ObservableCollection<Availability>()
            };

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

        public ValidatableObject<Address> Address
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

        public ValidatableObject<ObservableCollection<Availability>> Availability
        {
            get { return _availability; }
            set
            {
                _availability = value;
                RaisePropertyChanged();
            }
        }

        public IMvxCommand SelectTyreCommand => new MvxCommand(async () => await NavigateToSelectTyrePageAsync());

        public IMvxCommand SelectAddressCommand => new MvxCommand(async () => await NavigateToSelectAddressPageAsync());

        public IMvxCommand SelectAvailabilityCommand => new MvxCommand(async () => await NavigateToSelectSelectAvailabilityAsync());

        public IMvxCommand ValidateRegistrationCommand => new MvxCommand(() => ValidateRegistration());

        public IMvxCommand SubmitOrderCommand => new MvxCommand(async () => await SubmitOrderAsync());

        public override async Task Initialize()
        {
            await base.Initialize();
            try
            {
                Address.Value = await _addressService.GetPrimaryAddressAsync(Settings.AccessToken,
                    GlobalSetting.Instance.CurrentLoggedInUserId);
            }
            catch
            {
            }
        }

        private async Task SubmitOrderAsync()
        {
            IsBusy = true;

            if (Validate())
            {
                await _orderService.CreateNewPendingOrder(Settings.AccessToken, _order);
            }
            IsBusy = false;
        }

        private bool Validate()
        {
            var isRegistrationValid = ValidateRegistration();
            var isAddressValid = ValidateAddress();
            var isAvailabilityValid = ValidateAvailability();

            return isRegistrationValid && isAddressValid && isAvailabilityValid;
        }

        private async Task NavigateToSelectTyrePageAsync()
        {
            //ToDo Fix by using an actual object and not the whole order
            Order = await NavigationService.Navigate<SelectVehicalViewModel, CreateNewPendingOrderCommand, CreateNewPendingOrderCommand>(_order);
        }

        private async Task NavigateToSelectSelectAvailabilityAsync()
        {
            Availability = await NavigationService.Navigate<SelectAvailabilityViewModel, ValidatableObject<ObservableCollection<Availability>>, ValidatableObject<ObservableCollection<Availability>>>(new ValidatableObject<ObservableCollection<Availability>> { Value = new ObservableCollection<Availability>() });
        }

        private async Task NavigateToSelectAddressPageAsync()
        {
            Address.Value = await NavigationService.Navigate<SelectAddressViewModel, Address, Address>(new Address());
        }

        private bool ValidateRegistration() => _registration.Validate();

        private bool ValidateAddress() => _address.Validate();

        private bool ValidateAvailability() => _availability.Validate();

        private void AddValidations()
        {
            _registration.Validations.Add(new VehicleRegistrationRule<string> { ValidationMessage = "A valid UK vehical registration is required." });
            _address.Validations.Add((new IsNotNullOrEmptyRule<Address> { ValidationMessage = "A valid address is required." }));
            _availability.Validations.Add(new IsCountNotZeroRule<ObservableCollection<Availability>> { ValidationMessage = "At least 1 time slot is required." });
        }
    }
}