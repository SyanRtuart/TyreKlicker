using MediatR;
using System;

namespace TyreKlicker.Application.Order.Queries.GetOrder
{
    public class GetOrderQuery : IRequest<OrderDto>
    {
        public Guid Id { get; set; }
    }
}