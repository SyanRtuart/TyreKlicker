using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;

namespace TyreKlicker.XF.Core.ViewModels
{
    public class LoginViewModel : MvxNavigationViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly Services.IAppSettings _settings;

        public IMvxAsyncCommand LoginCommand { get; }
        public IMvxAsyncCommand NavigateToRegistrationPageCommand { get; }

        //public IMvxAsyncCommand LoginCommand { get; }

        public LoginViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, Services.IAppSettings settings) : base(logProvider, navigationService)
        {
            _navigationService = navigationService;
            _settings = settings;
            NavigateToRegistrationPageCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<RegistrationViewModel>());
            LoginCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<SplitRootViewModel>());
        }
    }
}