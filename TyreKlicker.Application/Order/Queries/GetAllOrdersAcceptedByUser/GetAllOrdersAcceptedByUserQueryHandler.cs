using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TyreKlicker.Persistence;

namespace TyreKlicker.Application.Order.Queries.GetAllOrdersAcceptedByUser
{
    internal class GetAllOrdersAcceptedByUserQueryHandler : IRequestHandler<GetAllOrdersAcceptedByUserQuery, OrderAcceptedByUserListViewModel>
    {
        private readonly TyreKlickerDbContext _context;

        public GetAllOrdersAcceptedByUserQueryHandler(TyreKlickerDbContext context)
        {
            _context = context;
        }

        public async Task<OrderAcceptedByUserListViewModel> Handle(GetAllOrdersAcceptedByUserQuery request, CancellationToken cancellationToken)
        {
            var model = new OrderAcceptedByUserListViewModel
            {
                Orders = await _context.Order
                .Where(x => x.AcceptedByUserId == request.UserId)
                .Select(OrderDto.Projection)
                .OrderBy(o => o.Registration)
                .ToListAsync(cancellationToken)
            };

            return model;
        }
    }
}