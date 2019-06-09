using MediatR;

namespace TyreKlicker.Application.Order.Queries.GetAllPendingOrders
{
    public class GetAllPendingOrdersQuery : IRequest<OrderListViewModel>
    {
    }
}