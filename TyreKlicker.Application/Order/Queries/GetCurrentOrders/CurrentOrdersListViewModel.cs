using System;
using System.Collections.Generic;
using System.Text;

namespace TyreKlicker.Application.Order.Queries.GetCurrentOrders
{
    public class CurrentOrdersListViewModel
    {
        public IEnumerable<OrderDto> Orders { get; set; }

    }
}
