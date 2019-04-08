using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
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
            get { return _addresses; }
            set
            {
                _addresses = value;
                RaisePropertyChanged(() => Addresses);
            }
        }

        public IMvxAsyncCommand NavigateToAddressDetailsCommand => new MvxAsyncCommand(async () => await NavigateToAddressDetailsAsync());

        public IMvxAsyncCommand NavigateToAddNewAddessCommand => new MvxAsyncCommand(async () => await NavigateToAddNewAddessAsync());

        private async Task NavigateToAddressDetailsAsync()
        {
            throw new System.NotImplementedException();
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            Addresses = await _addressService.GetAddressesAsync(Settings.AccessToken, GlobalSetting.Instance.CurrentLoggedInUserId);
        }

        public override void Prepare(Address parameter)
        {
        }
    }
}