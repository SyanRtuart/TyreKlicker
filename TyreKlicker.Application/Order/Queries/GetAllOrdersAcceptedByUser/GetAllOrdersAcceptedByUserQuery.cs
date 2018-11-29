using MediatR;
using System;

namespace TyreKlicker.Application.Order.Queries.GetAllOrdersAcceptedByUser
{
    public class GetAllOrdersAcceptedByUserQuery : IRequest<OrderAcceptedByUserListViewModel>
    {
        public Guid UserId { get; set; }
    }
}