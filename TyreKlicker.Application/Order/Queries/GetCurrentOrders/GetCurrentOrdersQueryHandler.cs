using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TyreKlicker.Persistence;

namespace TyreKlicker.Application.Order.Queries.GetCurrentOrders
{
    public class GetCurrentOrdersQueryHandler : IRequestHandler<GetCurrentOrdersQuery, CurrentOrdersListViewModel>
    {
        private readonly TyreKlickerDbContext _context;

        public GetCurrentOrdersQueryHandler(TyreKlickerDbContext context)
        {
            _context = context;
        }

        public async Task<CurrentOrdersListViewModel> Handle(GetCurrentOrdersQuery request, CancellationToken cancellationToken)
        {
            var model = new CurrentOrdersListViewModel
            {
                Orders = await _context.Order
                    .Where(x => x.Complete == false)
                    .Select(OrderDto.Projection)
                    .OrderBy(o => o.Registration)
                    .ToListAsync(cancellationToken)
            };

            return model;
        }
    }
}
