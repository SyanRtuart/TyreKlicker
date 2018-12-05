using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace TyreKlicker.Application.Order.Queries.GetCurrentOrders
{
    public class GetCurrentOrdersQuery : IRequest<CurrentOrdersListViewModel>
    {
        public Guid UserId { get; set; }
    }
}
