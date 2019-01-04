using System;

namespace TyreKlicker.XF.Core.Models.Order
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid CreatedByUserId { get; set; }
        public Guid AcceptedByUserId { get; set; }
        public string Registration { get; set; }
        public string Description { get; set; }
    }
}