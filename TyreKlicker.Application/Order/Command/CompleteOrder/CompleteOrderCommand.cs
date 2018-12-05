using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace TyreKlicker.Application.Order.Command.CompleteOrder
{
    public class CompleteOrderCommand : IRequest
    {
        public Guid OrderId { get; set; }

        public bool Complete { get; set; }
    }
}
