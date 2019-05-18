using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using System.Threading.Tasks;
using TyreKlicker.XF.Core.Helpers;
using TyreKlicker.XF.Core.Models.Address;
using TyreKlicker.XF.Core.Services.Address;
using TyreKlicker.XF.Core.Validators;

namespace TyreKlicker.XF.Core.ViewModels
{
    public class AddressViewModel : MvxNavigationViewModel<Address, Address>
    {
        private readonly IAddressService _addressService;

        private ValidatableObject<string> _street;
        private ValidatableObject<string> _city;
        private ValidatableObject<string> _postCode;
        private ValidatableObject<string> _phoneNumber;
        private bool _primaryAddress;

        public AddressViewModel(
            IMvxLogProvider logProvider,
            IMvxNavigationService navigationService,
            IAddressService addressService)
            : base(logProvider, navigationService)
        {
            _addressService = addressService;

            _street = new ValidatableObject<string>();
            _city = new ValidatableObject<string>();
            _phoneNumber = new ValidatableObject<string>();
            _postCode = new ValidatableObject<string>();

            AddValidations();
        }

        public ValidatableObject<string> Street
        {
            get => _street;
            set
            {
                _street = value;
                RaisePropertyChanged(() => Street);
            }
        }

        public ValidatableObject<string> City
        {
            get => _city;
            set
            {
                _city = value;
                RaisePropertyChanged(() => City);
            }
        }

        public ValidatableObject<string> PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                RaisePropertyChanged(() => PhoneNumber);
            }
        }

        public ValidatableObject<string> PostCode
        {
            get => _postCode;
            set
            {
                _postCode = value;
                RaisePropertyChanged(() => PostCode);
            }
        }

        public bool PrimaryAddress
        {
            get => _primaryAddress;
            set
            {
                _primaryAddress = value;
                RaisePropertyChanged(() => PrimaryAddress);
            }
        }

        public IMvxAsyncCommand SaveCommand => new MvxAsyncCommand(async () => await SaveAsync());

        private async Task SaveAsync()
        {
            IsBusy = true;

            if (Validate())
            {
                var createAddressCommand = new CreateAddressCommand()
                {
                    UserId = GlobalSetting.Instance.CurrentLoggedInUserId,
                    City = _city.Value,
                    PhoneNumber = _phoneNumber.Value,
                    Postcode = _postCode.Value,
                    Street = _street.Value,
                    PrimaryAddress = PrimaryAddress
                };

                await _addressService.CreateAddress(Settings.AccessToken, createAddressCommand);
                await NavigationService.Close(this, new Address
                {
                    City = _city.Value,
                    PhoneNumber = _phoneNumber.Value,
                    UserId = GlobalSetting.Instance.CurrentLoggedInUserId,
                    Postcode = _postCode.Value,
                    Street = _street.Value,
                    PrimaryAddress = PrimaryAddress
                });
            }

            IsBusy = false;
        }

        private bool Validate()
        {
            var isValidStreet = ValidateStreet();
            var isValidCity = ValidateCity();
            var isValidPhoneNumber = ValidatePhoneNumber();
            var isValidPostCode = ValidatePostCode();

            return isValidStreet && isValidCity && isValidPhoneNumber && isValidPostCode;
        }

        private bool ValidateStreet() => _street.Validate();

        private bool ValidateCity() => _city.Validate();

        private bool ValidatePhoneNumber() => _phoneNumber.Validate();

        private bool ValidatePostCode() => _postCode.Validate();

        private void AddValidations()
        {
            _street.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A street is required." });
            _city.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A city is required." });
            _phoneNumber.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A phone number is required." });
            _postCode.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A post code is required." });
        }

        public override void Prepare(Address address)
        {
            Street.Value = address.Street;
            City.Value = address.City;
            PhoneNumber.Value = address.PhoneNumber;
            PostCode.Value = address.Postcode;
            PrimaryAddress = address.PrimaryAddress;
        }
    }
}