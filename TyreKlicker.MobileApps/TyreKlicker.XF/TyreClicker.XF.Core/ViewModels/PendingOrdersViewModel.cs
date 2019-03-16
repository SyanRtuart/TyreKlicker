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
    public class PendingOrdersViewModel : MvxNavigationViewModel
    {
        private readonly IOrderService _orderService;
        private ObservableCollection<Order> _pendingOrders;

        public PendingOrdersViewModel(IMvxLogProvider logProvider,
            IMvxNavigationService navigationService,
            IOrderService orderService) : base(logProvider, navigationService)
        {
            _orderService = orderService;
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            await GetPendingOrdersCommand.ExecuteAsync();
        }

        public ObservableCollection<Order> PendingOrders
        {
            get { return _pendingOrders; }
            set
            {
                _pendingOrders = value;
                RaisePropertyChanged(() => PendingOrders);
            }
        }

        public IMvxAsyncCommand GetPendingOrdersCommand => new MvxAsyncCommand(async () =>
        {
            IsBusy = true;

            await GetPendingOrdersAsync();

            IsBusy = false;
        });

        public IMvxAsyncCommand NavigateToAddNewPendingOrderPageCommand
            => new MvxAsyncCommand(async () => await NavigateToAddNewPendingOrderPageAsync());

        public IMvxAsyncCommand<Order> NavigateToPendingOrderDetailsCommand
            => new MvxAsyncCommand<Order>(async (pendingOrder) => await NavigateToOrderDetailsAsync(pendingOrder));

        private async Task NavigateToOrderDetailsAsync(Order pendingOrder)
        {
            await NavigationService.Navigate<PendingOrderDetailsViewModel, Order>(pendingOrder);
        }

        private async Task NavigateToAddNewPendingOrderPageAsync()
        {
            await NavigationService.Navigate<NewPendingOrderViewModel>();
        }

        private async Task GetPendingOrdersAsync()
        {
            PendingOrders = await _orderService.GetAllPendingOrdersAsync(Settings.AccessToken);
        }
    }
}