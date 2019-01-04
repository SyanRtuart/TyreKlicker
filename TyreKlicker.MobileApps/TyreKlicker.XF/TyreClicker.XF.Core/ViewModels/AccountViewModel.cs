using MvvmCross.Logging;
using MvvmCross.Navigation;

namespace TyreKlicker.XF.Core.ViewModels
{
    public class AccountViewModel : MvxNavigationViewModel
    {
        public AccountViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }
    }
}