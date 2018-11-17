using System.Collections.Generic;

namespace TyreKlicker.Application.Order.Queries.GetAllPendingOrders
{
    public class OrderListViewModel
    {
        public IEnumerable<OrderDto> Orders { get; set; }

        //public bool CreateEnabled { get; set; }
    }
}