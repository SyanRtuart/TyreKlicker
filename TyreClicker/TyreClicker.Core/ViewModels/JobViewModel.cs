using MvvmCross.Logging;
using MvvmCross.Navigation;

namespace TyreKlicker.Core.ViewModels
{
    public class JobViewModel : MvxNavigationViewModel
    {
        public JobViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }
    }
}