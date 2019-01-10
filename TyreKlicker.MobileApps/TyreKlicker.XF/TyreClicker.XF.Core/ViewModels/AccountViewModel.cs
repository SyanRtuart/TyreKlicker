using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using System.Threading.Tasks;
using TyreKlicker.XF.Core.Helpers;

namespace TyreKlicker.XF.Core.ViewModels
{
    public class AccountViewModel : MvxNavigationViewModel
    {
        public AccountViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            LogoutCommand = new MvxAsyncCommand(async () => await LogoutAsync());
        }

        public IMvxAsyncCommand LogoutCommand { get; }

        private async Task LogoutAsync()
        {
            Settings.AccessToken = string.Empty;
            await NavigationService.Navigate<LoginViewModel>();
        }
    }
}