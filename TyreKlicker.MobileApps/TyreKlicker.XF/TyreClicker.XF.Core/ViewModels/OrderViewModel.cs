using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using TyreKlicker.XF.Core.Models.Order;

namespace TyreKlicker.XF.Core.ViewModels
{
    public class OrderViewModel : MvxNavigationViewModel
    {
        private MvxAsyncCommand _refreshCommand;

        public ICommand RefreshCommand => _refreshCommand = _refreshCommand ?? new MvxAsyncCommand(async () => await DoMyCommand());

        private async Task<OrderList> DoMyCommand()
        {
            // await  HttpClientService.Instance.GetItem<OrderListViewModel>("orders");
            return null;
        }

        public ObservableCollection<OrderList> StringItems { get; set; }

        public OrderViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            //RefreshCommand = new MvxAsyncCommand(async () => await StringItems = ApiClientFactory.Instance.GetOrders());
            //await DoMyCommand();
        }
    }
}