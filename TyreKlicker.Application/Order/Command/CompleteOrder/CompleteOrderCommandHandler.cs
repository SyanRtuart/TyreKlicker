using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TyreKlicker.Application.Exceptions;
using TyreKlicker.Persistence;

namespace TyreKlicker.Application.Order.Command.CompleteOrder
{
    public class CompleteOrderCommandHandler : IRequestHandler<CompleteOrderCommand, Unit>
    {
        private readonly TyreKlickerDbContext _context;

        public CompleteOrderCommandHandler(TyreKlickerDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CompleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _context.Order
                .SingleOrDefault(o => o.Id == request.OrderId);

            if (order == null) throw new NotFoundException(nameof(order), request.OrderId);

            order.Complete = request.Complete;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}