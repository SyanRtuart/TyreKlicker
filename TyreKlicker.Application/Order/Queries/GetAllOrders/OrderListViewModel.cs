﻿using System.Collections.Generic;

namespace TyreKlicker.Application.Order.Queries.GetAllOrders
{
    public class OrderListViewModel
    {
        public IEnumerable<OrderDto> Orders { get; set; }

        //public bool CreateEnabled { get; set; }
    }
}