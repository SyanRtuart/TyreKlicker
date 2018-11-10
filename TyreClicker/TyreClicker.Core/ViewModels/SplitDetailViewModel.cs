using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;

namespace TyreKlicker.Core.ViewModels
{
    public class SplitDetailViewModel : MvxNavigationViewModel
    {
        public SplitDetailViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            //ShowChildCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<SplitDetailNavViewModel>());
            //ShowTabsCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<TabsRootBViewModel>());
        }

        public IMvxAsyncCommand ShowChildCommand { get; private set; }
        public IMvxAsyncCommand ShowTabsCommand { get; private set; }

        public string ContentText => "Text for the Content Area";

        public override void ViewAppeared()
        {
            base.ViewAppeared();
        }
    }
}