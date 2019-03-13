using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TyreKlicker.Persistence;

namespace TyreKlicker.Application.Order.Queries.GetOrder
{
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderDto>
    {
        public readonly TyreKlickerDbContext _context;

        public GetOrderQueryHandler(TyreKlickerDbContext context)
        {
            _context = context;
        }

        public async Task<OrderDto> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var orderInDb = await _context.Order
                .Where(o => !o.AcceptedByUserId.HasValue)
                .Select(OrderDto.Projection)
                .OrderBy(o => o.Registration)
                .FirstOrDefaultAsync(cancellationToken);

            return orderInDb;
        }
    }
}