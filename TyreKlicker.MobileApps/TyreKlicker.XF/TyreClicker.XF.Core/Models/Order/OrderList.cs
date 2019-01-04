using System.Collections.Generic;

namespace TyreKlicker.XF.Core.Models.Order
{
    public class OrderList
    {
        public IEnumerable<Order> Orders { get; set; }
    }
}