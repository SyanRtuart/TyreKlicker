using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace TyreKlicker.Application.Order.Command.AcceptOrder
{
    public class AcceptOrderCommand : IRequest
    {
        public Guid OrderId { get; set; }

        public Guid UserId { get; set; }
    }
}
