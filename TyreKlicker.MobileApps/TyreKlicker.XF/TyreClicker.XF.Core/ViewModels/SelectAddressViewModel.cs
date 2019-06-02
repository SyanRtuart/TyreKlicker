using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using TyreKlicker.XF.Core.Helpers;
using TyreKlicker.XF.Core.Models.Address;
using TyreKlicker.XF.Core.Services.Address;

namespace TyreKlicker.XF.Core.ViewModels
{
    public class SelectAddressViewModel : MvxNavigationViewModel<Address, Address>
    {
        private readonly IAddressService _addressService;

        private ObservableCollection<Address> _addresses;

        public SelectAddressViewModel(IMvxLogProvider logProvider,
            IMvxNavigationService navigationService,
            IAddressService addressService)
            : base(logProvider, navigationService)
        {
            _addressService = addressService;
        }

        public ObservableCollection<Address> Addresses
        {
            get => _addresses;
            set
            {
                _addresses = value;
                RaisePropertyChanged(() => Addresses);
            }
        }

        public IMvxCommand<Address> SelectAddressCommand =>
            new MvxCommand<Address>(async address => await SelectAddressAsync(address));

        public IMvxCommand NavigateToAddNewAddressCommand =>
            new MvxCommand(async () => await NavigateToAddNewAddressAsync());

        private async Task NavigateToAddNewAddressAsync()
        {
            var address = await NavigationService.Navigate<AddressViewModel, Address, Address>(new Address());
            Addresses.Add(address);
        }

        private async Task SelectAddressAsync(Address address)
        {
            await NavigationService.Close(this, address);
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            Addresses = await _addressService.GetAddressesAsync(Settings.AccessToken,
                GlobalSetting.Instance.CurrentLoggedInUserId);
        }

        public override void Prepare(Address parameter)
        {
        }
    }
}