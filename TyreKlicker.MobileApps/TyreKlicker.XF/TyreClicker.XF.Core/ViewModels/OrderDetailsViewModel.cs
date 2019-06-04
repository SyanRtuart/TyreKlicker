using System.Threading.Tasks;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using TyreKlicker.XF.Core.Helpers;
using TyreKlicker.XF.Core.Models.Address;
using TyreKlicker.XF.Core.Models.Order;
using TyreKlicker.XF.Core.Services.Address;
using TyreKlicker.XF.Core.Services.Order;

namespace TyreKlicker.XF.Core.ViewModels
{
    public class OrderDetailsViewModel : MvxNavigationViewModel<Order>
    {
        private readonly IAddressService _addressService;
        private readonly IOrderService _orderService;

        private Address _address;
        private string _description;
        private Order _order;
        private string _registration;
        private Vehicle _vehicle;

        public OrderDetailsViewModel(IMvxLogProvider logProvider,
            IMvxNavigationService navigationService,
            IAddressService addressService, IOrderService orderService)
            : base(logProvider, navigationService)
        {
            _orderService = orderService;
            _addressService = addressService;
        }

        public Address Address
        {
            get => _address;
            set
            {
                _address = value;
                RaisePropertyChanged();
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                RaisePropertyChanged();
            }
        }

        public Order Order
        {
            get => _order;
            set
            {
                _order = value;
                RaisePropertyChanged();
            }
        }

        public string Registration
        {
            get => _registration;
            set
            {
                _registration = value;
                RaisePropertyChanged();
            }
        }

        public Vehicle Vehicle
        {
            get => _vehicle;
            set
            {
                _vehicle = value;
                RaisePropertyChanged();
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
            Address = await _addressService.GetAddressAsync(Settings.AccessToken, Order.AddressId);
        }
    }
}