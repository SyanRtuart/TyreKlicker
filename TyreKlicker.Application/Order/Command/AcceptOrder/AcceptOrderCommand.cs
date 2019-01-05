using MediatR;
using System;

namespace TyreKlicker.Application.Order.Command.AcceptOrder
{
    public class AcceptOrderCommand : IRequest
    {
        public Guid UserId { get; set; }

        public Guid OrderId { get; set; }
    }
}