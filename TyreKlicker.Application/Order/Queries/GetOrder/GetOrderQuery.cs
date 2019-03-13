using MediatR;

namespace TyreKlicker.Application.Order.Queries.GetOrder
{
    public class GetOrderQuery : IRequest<OrderDto>
    {
        public string Id { get; set; }
    }
}