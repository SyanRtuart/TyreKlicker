using System.Collections.Generic;

namespace TyreKlicker.Application.Order.Queries.GetCurrentOrders
{
    public class CurrentOrdersListViewModel
    {
        public IEnumerable<OrderDto> Orders { get; set; }
    }
}