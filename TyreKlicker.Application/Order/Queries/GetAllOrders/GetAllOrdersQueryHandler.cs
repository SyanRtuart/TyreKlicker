﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TyreKlicker.Persistence;

namespace TyreKlicker.Application.Order.Queries.GetAllOrders
{
    public class GetAllPendingOrdersQueryHandler : IRequestHandler<GetAllOrders, OrderListViewModel>
    {
        private readonly TyreKlickerDbContext _context;

        public GetAllPendingOrdersQueryHandler(TyreKlickerDbContext context)
        {
            _context = context;
        }

        public async Task<OrderListViewModel> Handle(GetAllOrders request, CancellationToken cancellationToken)
        {
            var model = new OrderListViewModel
            {
                Orders = await _context.Order
                .Select(OrderDto.Projection)
                .OrderBy(o => o.Registration)
                .ToListAsync(cancellationToken)
            };

            return model;
        }
    }
}