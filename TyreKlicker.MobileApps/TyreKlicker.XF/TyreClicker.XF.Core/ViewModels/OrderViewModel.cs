using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TyreKlicker.XF.Core.Helpers;
using TyreKlicker.XF.Core.Models.Order;
using TyreKlicker.XF.Core.Services.Order;

namespace TyreKlicker.XF.Core.ViewModels
{
    public class OrderViewModel : MvxNavigationViewModel
    {
        private readonly IOrderService _orderService;
        private ObservableCollection<Order> _orderItems;

        public OrderViewModel(IMvxLogProvider logProvider,
            IMvxNavigationService navigationService,
            IOrderService orderService) : base(logProvider, navigationService)
        {
            _orderService = orderService;
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            await GetOrdersAsync();
            // do the heavy work here
        }

        public ObservableCollection<Order> OrderItems
        {
            get { return _orderItems; }
            set
            {
                _orderItems = value;
                RaisePropertyChanged(() => OrderItems);
            }
        }

        public IMvxCommand<Order> OpenOrderDetailsCommand => new MvxCommand<Order>(async (order) => await OpenOrderDetailsAsync(order));

        public IMvxCommand GetOrdersCommand => new MvxCommand(async () =>
        {
            IsBusy = true;

            await GetOrdersAsync();

            IsBusy = false;
        });

        private async Task OpenOrderDetailsAsync(Order order)
        {
            await NavigationService.Navigate<OrderDetailsViewModel, Order>(order);
        }

        private async Task GetOrdersAsync()
        {
            //ToDo if there are 0 items show something on the screen
            OrderItems = await _orderService.GetOrders(Settings.AccessToken, GlobalSetting.Instance.CurrentLoggedInUserId);
        }
    }
}