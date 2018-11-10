using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TyreKlicker.Application.Exceptions;
using TyreKlicker.Domain.Entities;
using TyreKlicker.Persistence;

namespace TyreKlicker.Application.Order.Queries
{
    public class GetAllOrdersHandler : IRequestHandler<GetAllOrdersQuery, OrderListViewModel>
    {
        private readonly TyreKlickerDbContext _context;

        public GetAllOrdersHandler(TyreKlickerDbContext context)
        {
            _context = context;
        }

        public async Task<OrderListViewModel> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            // TODO ADD ME
            //if (entity == null)
            //{
            //    throw new NotFoundException(nameof(Customer), request.Id);
            //}

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