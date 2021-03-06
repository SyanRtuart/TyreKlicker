﻿using System;
using System.Collections.Generic;

namespace TyreKlicker.XF.Core.Models.Order
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid AddressId { get; set; }
        public Guid CreatedByUserId { get; set; }
        public Guid AcceptedByUserId { get; set; }
        public string Registration { get; set; }
        public string Description { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string Trim { get; set; }
        public string Tyre { get; set; }
        public IEnumerable<Availability> Availability { get; set; }
    }
}