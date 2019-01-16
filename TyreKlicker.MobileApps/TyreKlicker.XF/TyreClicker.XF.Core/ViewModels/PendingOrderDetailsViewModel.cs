using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using System.Threading.Tasks;
using TyreKlicker.XF.Core.Helpers;
using TyreKlicker.XF.Core.Models.Order;
using TyreKlicker.XF.Core.Services.Order;
using TyreKlicker.XF.Core.Services.User;

namespace TyreKlicker.XF.Core.ViewModels
{
    public class PendingOrderDetailsViewModel : MvxNavigationViewModel<Order>
    {
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;

        private Order _pendingOrder;

        public PendingOrderDetailsViewModel(IMvxLogProvider logProvider,
            IMvxNavigationService navigationService,
            IOrderService orderService,
            IUserService userService) : base(logProvider, navigationService)
        {
            _orderService = orderService;
            _userService = userService;
            AcceptOrderCommand = new MvxAsyncCommand(async () => await AcceptOrderAsync());
        }

        private async Task AcceptOrderAsync()
        {
            var user = await _userService.GetUser(GlobalSetting.Instance.CurrentLoggedInUserEmail);

            var command = new AcceptOrderCommand { OrderId = _pendingOrder.Id, UserId = user.Id };

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

        public IMvxAsyncCommand AcceptOrderCommand { get; }

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