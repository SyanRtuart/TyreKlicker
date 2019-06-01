using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TyreKlicker.Persistence;

namespace TyreKlicker.Application.Order.Queries.GetAllOrdersCreatedByUser
{
    public class GetAllOrdersCreatedUserQueryHandler : IRequestHandler<GetAllOrdersCreatedByUserQuery, OrderCreatedByUserListViewModel>
    {
        private readonly TyreKlickerDbContext _context;

        public GetAllOrdersCreatedUserQueryHandler(TyreKlickerDbContext context)
        {
            _context = context;
        }

        public async Task<OrderCreatedByUserListViewModel> Handle(GetAllOrdersCreatedByUserQuery request, CancellationToken cancellationToken)
        {
            var model = new OrderCreatedByUserListViewModel
            {
                Orders = await _context.Order
                .Where(x => x.CreatedBy == request.UserId)
                .Select(OrderDto.Projection)
                .OrderBy(o => o.Registration)
                .ToListAsync(cancellationToken)
            };

            return model;
        }
    }
}