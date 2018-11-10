using MvvmCross.Commands;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Logging;
using MvvmCross.Navigation;

namespace TyreKlicker.Core.ViewModels
{
    [MvxModalPresentation()]
    public class RegistrationViewModel : MvxNavigationViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly Services.IAppSettings _settings;

        public RegistrationViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, Services.IAppSettings settings) : base(logProvider, navigationService)
        {
            _navigationService = navigationService;
            _settings = settings;
        }
    }
}