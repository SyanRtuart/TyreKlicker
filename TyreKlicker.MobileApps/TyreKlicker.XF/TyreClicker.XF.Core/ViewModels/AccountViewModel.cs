using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using TyreKlicker.XF.Core.Helpers;

namespace TyreKlicker.XF.Core.ViewModels
{
    public class AccountViewModel : MvxNavigationViewModel
    {
        public AccountViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(
            logProvider, navigationService)
        {
            LogoutCommand = new MvxCommand(async () => await LogoutAsync());
        }

        public IMvxCommand LogoutCommand { get; }

        private async Task LogoutAsync()
        {
            Settings.AccessToken = string.Empty;
            await NavigationService.Navigate<LoginViewModel>();
        }
    }
}