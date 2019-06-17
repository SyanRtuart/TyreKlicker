using MediatR;
using System;
using TyreKlicker.Application.Order.Models;

namespace TyreKlicker.Application.Order.Queries.GetOrder
{
    public class GetOrderQuery : IRequest<OrderDto>
    {
        public Guid Id { get; set; }
    }
}