using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using System;
using System.Linq;
using System.Threading.Tasks;
using TyreKlicker.XF.Core.Helpers;
using TyreKlicker.XF.Core.Models.Address;
using TyreKlicker.XF.Core.Services.Address;
using TyreKlicker.XF.Core.Services.Tyre;

namespace TyreKlicker.XF.Core.ViewModels
{
    public class TestingViewModel : MvxNavigationViewModel
    {
        private readonly ITyreService _tyreService;
        private readonly IAddressService _addressService;

        private Address _address;

        public Address Address
        {
            get => _address;
            set
            {
                _address = value;
                RaisePropertyChanged(() => Address);
            }
        }

        public TestingViewModel(IMvxLogProvider logProvider,
            IMvxNavigationService navigationService,
            ITyreService tyreService, IAddressService addressService)
            : base(logProvider, navigationService)
        {
            _tyreService = tyreService;
            _addressService = addressService;
            Button1Command = new MvxCommand(async () => await Button1Execute());
            Button2Command = new MvxCommand(async () => await Button2Execute());
            Button3Command = new MvxCommand(async () => await Button3Execute());
            Button4Command = new MvxCommand(async () => await Button4Execute());
            Button5Command = new MvxCommand(async () => await Button5Execute());
            Button6Command = new MvxCommand(async () => await Button6Execute());

            Address = new Address
            {
                UserId = Guid.NewGuid(),
                PrimaryAddress = true,
                Postcode = "G23 5HU",
                Street = "Smith Street",
                City = "Large City",
                PhoneNumber = "115151"
            };
        }

        private async Task Button1Execute()
        {
            var makes = await _tyreService.GetMakes();
            var thisMake = makes.First(make => make.Name == "Audi");

            var models = await _tyreService.GetModels(thisMake);
            var thisModel = models.First(model => model.Name == "A4");

            var years = await _tyreService.GetYears(thisMake);
            var thisYear = years.FirstOrDefault();

            var tyres = await _tyreService.GetVehicles(thisMake, thisModel, thisYear);
            var thisTyre = tyres.FirstOrDefault();
        }

        private async Task Button2Execute()
        {
            var result = await _addressService.GetAddressAsync(Settings.AccessToken, Guid.NewGuid());
        }

        private async Task Button3Execute()
        {
            var command = new CreateAddressCommand
            {
                UserId = GlobalSetting.Instance.CurrentLoggedInUserId,
                Street = "street",
                City = "city",
                Postcode = "PC",
                PhoneNumber = "0101401401",
                PrimaryAddress = true
            };
            var result = await _addressService.CreateAddress(Settings.AccessToken, command);

            var john = result.UserId;
        }

        private async Task Button4Execute()
        {
            var result = await _addressService.GetPrimaryAddressAsync(Settings.AccessToken, GlobalSetting.Instance.CurrentLoggedInUserId);
            var asdas = "";
        }

        private async Task Button5Execute()
        {
            throw new System.NotImplementedException();
        }

        private async Task Button6Execute()
        {
            throw new System.NotImplementedException();
        }

        public IMvxCommand Button1Command { get; }
        public IMvxCommand Button2Command { get; }
        public IMvxCommand Button3Command { get; }
        public IMvxCommand Button4Command { get; }
        public IMvxCommand Button5Command { get; }
        public IMvxCommand Button6Command { get; }
    }
}