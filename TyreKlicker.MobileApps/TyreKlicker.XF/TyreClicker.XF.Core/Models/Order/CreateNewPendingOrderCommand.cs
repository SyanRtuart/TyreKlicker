using System;

namespace TyreKlicker.XF.Core.Models.Order
{
    public class CreateNewPendingOrderCommand
    {
        public Guid CreatedByUserId { get; set; }
        public string Registration { get; set; }
        public string Description { get; set; }
    }
}