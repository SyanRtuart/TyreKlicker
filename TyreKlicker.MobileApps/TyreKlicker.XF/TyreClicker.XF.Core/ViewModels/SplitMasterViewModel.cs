using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using System.Collections.ObjectModel;
using TyreKlicker.XF.Core.Controls;

namespace TyreKlicker.XF.Core.ViewModels
{
    public class SplitMasterViewModel : MvxNavigationViewModel
    {
        public ObservableCollection<MenuItem> MenuItems { get; set; }

        public SplitMasterViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            //TO DO Load Menu Items from User
            //Save it to the phome aswell?
            MenuItems = new ObservableCollection<MenuItem>
            {
                new MenuItem { Label = MenuItem.Order  , IconUniCode = Resources.FontAwesome.Solid.Briefcase },
                new MenuItem { Label = MenuItem.JobBoard, IconUniCode = Resources.FontAwesome.Solid.Clipboard },
                new MenuItem { Label = MenuItem.News, IconUniCode = Resources.FontAwesome.Solid.Newspaper },
                new MenuItem { Label = MenuItem.Account, IconUniCode = Resources.FontAwesome.Solid.User },
            };
        }

        public IMvxCommand OpenUrlCommand =>
            new MvxCommand<MenuItem>(async (menuItem) =>
            {
                switch (menuItem.Label)
                {
                    case MenuItem.Order:
                        await NavigationService.Navigate<OrderViewModel>();
                        break;

                    case MenuItem.JobBoard:
                        await NavigationService.Navigate<PendingOrdersViewModel>();
                        break;

                    case MenuItem.News:
                        await NavigationService.Navigate<NewsViewModel>();
                        break;

                    case MenuItem.Account:
                        await NavigationService.Navigate<AccountViewModel>();
                        break;

                    default:
                        break;
                }
            });

        public override void ViewAppeared()
        {
            base.ViewAppeared();
        }
    }
}