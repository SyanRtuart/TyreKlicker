using System.Collections.Generic;

namespace TyreKlicker.Application.Order.Queries.GetAllOrdersCreatedByUser
{
    public class OrderCreatedByUserListViewModel
    {
        public IEnumerable<OrderDto> Orders { get; set; }

        //public bool CreateEnabled { get; set; }
    }
}