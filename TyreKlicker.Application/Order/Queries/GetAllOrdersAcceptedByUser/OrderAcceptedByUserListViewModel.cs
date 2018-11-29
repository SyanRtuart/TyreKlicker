using System.Collections.Generic;

namespace TyreKlicker.Application.Order.Queries.GetAllOrdersAcceptedByUser
{
    public class OrderAcceptedByUserListViewModel
    {
        public IEnumerable<OrderDto> Orders { get; set; }
    }
}