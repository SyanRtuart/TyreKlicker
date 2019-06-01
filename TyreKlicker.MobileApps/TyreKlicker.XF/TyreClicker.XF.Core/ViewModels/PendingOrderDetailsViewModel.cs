using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using System.Threading.Tasks;
using TyreKlicker.XF.Core.Helpers;
using TyreKlicker.XF.Core.Models.Order;
using TyreKlicker.XF.Core.Services.Order;

namespace TyreKlicker.XF.Core.ViewModels
{
    public class PendingOrderDetailsViewModel : MvxNavigationViewModel<Order>
    {
        private readonly IOrderService _orderService;

        private Order _pendingOrder;

        public PendingOrderDetailsViewModel(IMvxLogProvider logProvider,
            IMvxNavigationService navigationService,
            IOrderService orderService)
            : base(logProvider, navigationService)
        {
            _orderService = orderService;
            AcceptOrderCommand = new MvxCommand(async () => await AcceptOrderAsync());
        }

        private async Task AcceptOrderAsync()
        {
            var command = new AcceptOrderCommand { OrderId = _pendingOrder.Id, UserId = GlobalSetting.Instance.CurrentLoggedInUserId };

            await _orderService.AcceptOrder(Settings.AccessToken, command);
        }

        public Order PendingOrder
        {
            get => _pendingOrder;
            set
            {
                _pendingOrder = value;
                RaisePropertyChanged(() => PendingOrder);
            }
        }

        public IMvxCommand AcceptOrderCommand { get; }

        public override void Prepare()
        {
            // first callback. Initialize parameter-agnostic stuff here
        }

        public override void Prepare(Order pendingOrder)
        {
            PendingOrder = pendingOrder;
        }

        public override async Task Initialize()
        {
            await base.Initialize();

            // do the heavy work here
        }
    }
}