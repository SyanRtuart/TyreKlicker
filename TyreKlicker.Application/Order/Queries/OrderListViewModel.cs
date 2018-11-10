using System;
using System.Collections.Generic;
using System.Text;

namespace TyreKlicker.Application.Order.Queries
{
    public class OrderListViewModel
    {
        //TODO Intorduce DRTO
        public IEnumerable<OrderDto> Orders { get; set; }

        public bool CreateEnabled { get; set; }
    }
}