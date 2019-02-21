using MediatR;
using System;

namespace TyreKlicker.Application.Order.Command.CreateOrder
{
    public class CreateOrderCommand : IRequest
    {
        public Guid CreatedByUserId { get; set; }

        public string Registration { get; set; }

        public string Description { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string Year { get; set; }

        public string Trim { get; set; }

        public string Tyre { get; set; }
    }
}