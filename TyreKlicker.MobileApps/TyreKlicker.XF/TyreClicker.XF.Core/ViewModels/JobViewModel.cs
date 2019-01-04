using MvvmCross.Logging;
using MvvmCross.Navigation;

namespace TyreKlicker.XF.Core.ViewModels
{
    public class JobViewModel : MvxNavigationViewModel
    {
        public JobViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }
    }
}