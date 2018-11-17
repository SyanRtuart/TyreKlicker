using MediatR;

namespace TyreKlicker.Application.Order.Queries.GetAllOrders
{
    public class GetAllOrdersQuery : IRequest<OrderListViewModel>
    {
        public string Id { get; set; }
    }
}