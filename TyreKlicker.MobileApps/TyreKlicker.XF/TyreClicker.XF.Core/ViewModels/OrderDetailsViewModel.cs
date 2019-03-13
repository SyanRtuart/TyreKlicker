using MvvmCross.Logging;
using MvvmCross.Navigation;
using System.Threading.Tasks;
using TyreKlicker.XF.Core.Helpers;
using TyreKlicker.XF.Core.Models.Order;
using TyreKlicker.XF.Core.Services.Order;

namespace TyreKlicker.XF.Core.ViewModels
{
    public class OrderDetailsViewModel : MvxNavigationViewModel<Order>
    {
        private readonly IOrderService _orderService;

        private Order _order;

        public OrderDetailsViewModel(IMvxLogProvider logProvider,
            IMvxNavigationService navigationService,
            IOrderService orderService)
            : base(logProvider, navigationService)
        {
            _orderService = orderService;
        }

        public Order Order
        {
            get => _order;
            set
            {
                _order = value;
                RaisePropertyChanged(() => Order);
            }
        }

        public override void Prepare()
        {
            // first callback. Initialize parameter-agnostic stuff here
        }

        public override void Prepare(Order order)
        {
            _order = order;
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            Order = await _orderService.GetOrder(Settings.AccessToken, _order.Id);
            // do the heavy work here
        }
    }
}