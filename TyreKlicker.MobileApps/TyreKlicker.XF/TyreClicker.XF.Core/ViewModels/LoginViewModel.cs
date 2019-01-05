using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using System.Threading.Tasks;
using TyreKlicker.XF.Core.Services.AuthenticationService;

namespace TyreKlicker.XF.Core.ViewModels
{
    public class LoginViewModel : MvxNavigationViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly Services.IAppSettings _settings;
        private readonly IAuthenticationService _authenticationService;

        public LoginViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService,
            Services.IAppSettings settings, IAuthenticationService authenticationService)
            : base(logProvider, navigationService)
        {
            _authenticationService = authenticationService;
            _navigationService = navigationService;
            _settings = settings;
            NavigateToRegistrationPageCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<RegistrationViewModel>());
            LoginCommand = new MvxAsyncCommand(async () => await LoginAsync());

            //LoginCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<SplitRootViewModel>());
        }

        private async Task LoginAsync()
        {
            throw new System.NotImplementedException();
        }

        public IMvxAsyncCommand LoginCommand { get; }
        public IMvxAsyncCommand NavigateToRegistrationPageCommand { get; }

        //public IMvxAsyncCommand LoginCommand { get; }
    }
}