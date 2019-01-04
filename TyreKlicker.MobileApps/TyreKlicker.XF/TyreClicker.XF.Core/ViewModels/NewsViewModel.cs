using MvvmCross.Logging;
using MvvmCross.Navigation;

namespace TyreKlicker.XF.Core.ViewModels
{
    public class NewsViewModel : MvxNavigationViewModel
    {
        public NewsViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }
    }
}