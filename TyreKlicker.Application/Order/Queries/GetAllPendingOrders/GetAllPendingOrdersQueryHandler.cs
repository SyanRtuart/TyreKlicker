using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TyreKlicker.Persistence;

namespace TyreKlicker.Application.Order.Queries.GetAllPendingOrders
{
    public class GetAllPendingOrdersQueryHandler : IRequestHandler<GetAllPendingOrdersQuery, OrderListViewModel>
    {
        private readonly TyreKlickerDbContext _context;

        public GetAllPendingOrdersQueryHandler(TyreKlickerDbContext context)
        {
            _context = context;
        }

        public async Task<OrderListViewModel> Handle(GetAllPendingOrdersQuery request, CancellationToken cancellationToken)
        {

            var model = new OrderListViewModel
            {
                Orders = await _context.Order
                .Where(o => o.AcceptedByUserId == Guid.Empty)
                .Select(OrderDto.Projection)
                .OrderBy(o => o.Registration)
                .ToListAsync(cancellationToken)
            };

            return model;
        }
    }
}