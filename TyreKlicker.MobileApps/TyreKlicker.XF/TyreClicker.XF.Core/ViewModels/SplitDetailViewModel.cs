using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;

namespace TyreKlicker.XF.Core.ViewModels
{
    public class SplitDetailViewModel : MvxNavigationViewModel
    {
        public SplitDetailViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            //ShowChildCommand = new MvxCommand(async () => await NavigationService.Navigate<SplitDetailNavViewModel>());
            //ShowTabsCommand = new MvxCommand(async () => await NavigationService.Navigate<TabsRootBViewModel>());
        }

        public IMvxCommand ShowChildCommand { get; private set; }
        public IMvxCommand ShowTabsCommand { get; private set; }

        public string ContentText => "Text for the Content Area";

        public override void ViewAppeared()
        {
            base.ViewAppeared();
        }
    }
}