using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TyreKlicker.XF.Core.Models.Order;

namespace TyreKlicker.XF.Core.Services.Order
{
    public interface IOrderService
    {
        Task<ObservableCollection<Models.Order.Order>> GetAllPendingOrdersAsync(string token);

        Task<ObservableCollection<Models.Order.Order>> GetOrders(string token, Guid userId);

        Task<Models.Order.Order> GetOrder(string token, Guid OrderId);

        Task<AcceptOrderCommand> AcceptOrder(string token, AcceptOrderCommand command);

        Task<CreateNewPendingOrderCommand> CreateNewPendingOrder(string token, CreateNewPendingOrderCommand command);
    }
}