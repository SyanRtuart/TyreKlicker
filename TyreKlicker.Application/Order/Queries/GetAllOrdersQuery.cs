using MediatR;

namespace TyreKlicker.Application.Order.Queries
{
    public class GetAllOrdersQuery : IRequest<OrderListViewModel>
    {
        public string Id { get; set; }
    }
}