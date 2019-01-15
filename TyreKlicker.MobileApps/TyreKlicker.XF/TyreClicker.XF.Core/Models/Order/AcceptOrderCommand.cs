using System;

namespace TyreKlicker.XF.Core.Models.Order
{
    public class AcceptOrderCommand
    {
        public Guid UserId { get; set; }
        public Guid OrderId { get; set; }
    }
}