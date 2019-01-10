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

        //private MvxAsyncCommand _refreshCommand;
        public IMvxAsyncCommand GetOrdersCommand { get; }

        public ObservableCollection<Order> OrderItems
        {
            get { return _orderItems; }
            set
            {
                _orderItems = value;
                RaisePropertyChanged(() => OrderItems);
            }
        }

        public OrderViewModel(IMvxLogProvider logProvider,
            IMvxNavigationService navigationService,
            IOrderService orderService) : base(logProvider, navigationService)
        {
            GetOrdersCommand = new MvxAsyncCommand(async () => await GetOrdersAsync());
            _orderService = orderService;
        }

        private async Task GetOrdersAsync()
        {
            OrderItems = await _orderService.GetAllPendingOrdersAsync(Settings.AccessToken);
        }
    }
}