using System;
using MediatR;

namespace TyreKlicker.Application.Order.Command.CreateOrder
{
    public class CreateOrderCommand : IRequest
    {
        public Guid CreatedByUserId { get; set; }

        public string Registration { get; set; }

        public string Description { get; set; }
    }
}