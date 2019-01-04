using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace TyreKlicker.XF.Core.Services.Order
{
    public interface IOrderService
    {
        Task<ObservableCollection<Models.Order.OrderList>> GetOrdersAsync(string token);
    }
}