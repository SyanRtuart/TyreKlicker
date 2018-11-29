using MediatR;
using System;

namespace TyreKlicker.Application.Order.Queries.GetAllOrdersCreatedByUser
{
    public class GetAllOrdersCreatedByUserQuery : IRequest<OrderCreatedByUserListViewModel>
    {
        public Guid UserId { get; set; }
    }
}