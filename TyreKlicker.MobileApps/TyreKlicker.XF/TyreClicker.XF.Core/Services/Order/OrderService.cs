﻿using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TyreKlicker.XF.Core.Extensions;
using TyreKlicker.XF.Core.Helpers;
using TyreKlicker.XF.Core.Models.Order;
using TyreKlicker.XF.Core.Services.RequestProvider;

namespace TyreKlicker.XF.Core.Services.Order
{
    public class OrderService : IOrderService
    {
        private readonly IRequestProvider _requestProvider;

        private const string ApiUrlBase = "api/order";

        public OrderService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<ObservableCollection<Models.Order.Order>> GetAllPendingOrdersAsync(string token)
        {
            var uri = UriHelper.CombineUri(GlobalSetting.Instance.BaseIdentityEndpoint, $"{ApiUrlBase}");

            var orders = await _requestProvider.GetAsync<OrderList>(uri,
                Settings.AccessToken);

            if (orders?.Orders != null)
            {
                return orders?.Orders.ToObservableCollection();
            }

            return new ObservableCollection<Models.Order.Order>();
        }

        public async Task<AcceptOrderCommand> AcceptOrder(string token, AcceptOrderCommand command)
        {
            var uri = UriHelper.CombineUri(GlobalSetting.Instance.BaseIdentityEndpoint, $"{ApiUrlBase}/{command.OrderId}/AcceptOrder");

            var result = await _requestProvider.PostAsync(uri, command, Settings.AccessToken);

            return result;
        }

        public async Task<CreateNewPendingOrderCommand> CreateNewPendingOrder(string token, CreateNewPendingOrderCommand command)
        {
            var uri = UriHelper.CombineUri(GlobalSetting.Instance.BaseIdentityEndpoint, $"{ApiUrlBase}");

            var result = await _requestProvider.PostAsync(uri, command, Settings.AccessToken);

            return result;
        }
    }
}