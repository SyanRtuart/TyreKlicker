using MediatR;

namespace TyreKlicker.Application.Order.Queries.GetAllOrders
{
    public class GetAllOrders : IRequest<OrderListViewModel>
    {
        public string Id { get; set; }
    }
}