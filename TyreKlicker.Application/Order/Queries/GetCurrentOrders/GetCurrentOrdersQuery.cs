using MediatR;
using System;

namespace TyreKlicker.Application.Order.Queries.GetCurrentOrders
{
    public class GetCurrentOrdersQuery : IRequest<CurrentOrdersListViewModel>
    {
        public Guid UserId { get; set; }
    }
}