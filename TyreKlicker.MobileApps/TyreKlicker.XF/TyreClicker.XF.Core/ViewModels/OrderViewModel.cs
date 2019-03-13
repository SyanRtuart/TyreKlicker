﻿using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TyreKlicker.XF.Core.Helpers;
using TyreKlicker.XF.Core.Models.Order;
using TyreKlicker.XF.Core.Services.Order;

namespace TyreKlicker.XF.Core.ViewModels
{
    public class OrderViewModel : MvxNavigationViewModel
    {
        private readonly IOrderService _orderService;
        private ObservableCollection<Order> _orderItems;

        public OrderViewModel(IMvxLogProvider logProvider,
            IMvxNavigationService navigationService,
            IOrderService orderService) : base(logProvider, navigationService)
        {
            _orderService = orderService;
            GetOrdersCommand = new MvxAsyncCommand(async () => await GetOrdersAsync());
        }

        public ObservableCollection<Order> OrderItems
        {
            get { return _orderItems; }
            set
            {
                _orderItems = value;
                RaisePropertyChanged(() => OrderItems);
            }
        }

        public IMvxAsyncCommand GetOrdersCommand { get; }

        public IMvxAsyncCommand<Order> OpenOrderDetailsCommand => new MvxAsyncCommand<Order>(async (order) => await OpenOrderDetailsAsync(order));

        private async Task OpenOrderDetailsAsync(Order order)
        {
            await NavigationService.Navigate<OrderDetailsViewModel, Order>(order);
        }

        private async Task GetOrdersAsync()
        {
            OrderItems = await _orderService.GetOrders(Settings.AccessToken, GlobalSetting.Instance.CurrentLoggedInUserId);
        }
    }
}