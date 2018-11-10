using MvvmCross.Logging;
using MvvmCross.Navigation;

namespace TyreKlicker.Core.ViewModels
{
    public class OrderViewModel : MvxNavigationViewModel
    {
        public OrderViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }
    }
}