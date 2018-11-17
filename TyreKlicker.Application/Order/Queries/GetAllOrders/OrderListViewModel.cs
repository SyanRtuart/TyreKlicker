using System.Collections.Generic;

namespace TyreKlicker.Application.Order.Queries.GetAllOrders
{
    public class OrderListViewModel
    {
        //TODO Intorduce DRTO
        public IEnumerable<OrderDto> Orders { get; set; }

        public bool CreateEnabled { get; set; }
    }
}