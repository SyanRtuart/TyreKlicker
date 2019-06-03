using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using TyreKlicker.XF.Core.Helpers;
using TyreKlicker.XF.Core.Models.Address;
using TyreKlicker.XF.Core.Models.Order;
using TyreKlicker.XF.Core.Services.Address;
using TyreKlicker.XF.Core.Services.Order;

namespace TyreKlicker.XF.Core.ViewModels
{
    public class PendingOrderDetailsViewModel : MvxNavigationViewModel<Order>
    {
        private readonly IAddressService _addressService;
        private readonly IOrderService _orderService;
        private Address _address;
        private string _description;

        private Order _pendingOrder;
        private string _registration;
        private Vehicle _vehicle;

        public PendingOrderDetailsViewModel(IMvxLogProvider logProvider,
            IMvxNavigationService navigationService,
            IOrderService orderService, IAddressService addressService)
            : base(logProvider, navigationService)
        {
            _orderService = orderService;
            _addressService = addressService;
            AcceptOrderCommand = new MvxCommand(async () => await AcceptOrderAsync());
        }

        public Order PendingOrder
        {
            get => _pendingOrder;
            set
            {
                _pendingOrder = value;
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


        public IMvxCommand AcceptOrderCommand { get; }

        private async Task AcceptOrderAsync()
        {
            var command = new AcceptOrderCommand
                {OrderId = _pendingOrder.Id, UserId = GlobalSetting.Instance.CurrentLoggedInUserId};

            await _orderService.AcceptOrder(Settings.AccessToken, command);
            await NavigationService.Close(this);
        }

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
            PendingOrder = await _orderService.GetOrder(Settings.AccessToken, _pendingOrder.Id);
            Address = await _addressService.GetAddressAsync(Settings.AccessToken, PendingOrder.AddressId);


            // do the heavy work here
        }
    }
}