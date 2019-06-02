using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
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

        public ObservableCollection<Order> PendingOrders
        {
            get => _pendingOrders;
            set
            {
                _pendingOrders = value;
                RaisePropertyChanged(() => PendingOrders);
            }
        }

        public IMvxCommand GetPendingOrdersCommand => new MvxCommand(async () =>
        {
            IsBusy = true;

            await GetPendingOrdersAsync();

            IsBusy = false;
        });

        public IMvxCommand NavigateToAddNewPendingOrderPageCommand
            => new MvxCommand(async () => await NavigateToAddNewPendingOrderPageAsync());

        public IMvxCommand<Order> NavigateToPendingOrderDetailsCommand
            => new MvxCommand<Order>(async pendingOrder => await NavigateToOrderDetailsAsync(pendingOrder));

        public override async Task Initialize()
        {
            await base.Initialize();
            await GetPendingOrdersAsync();
        }

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