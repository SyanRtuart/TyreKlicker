using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TyreKlicker.Application.Exceptions;
using TyreKlicker.Persistence;

namespace TyreKlicker.Application.Order.Command.AcceptOrder
{
    public class AcceptOrderCommandHandler : IRequestHandler<AcceptOrderCommand, Unit>
    {
        private readonly TyreKlickerDbContext _context;

        public AcceptOrderCommandHandler(TyreKlickerDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AcceptOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _context.Order.FirstOrDefault(x => x.Id == request.OrderId);
            if (order == null) throw new NotFoundException(nameof(order), request.OrderId);

            if (order.AcceptedByUserId.HasValue && order.AcceptedByUserId != Guid.Empty)
                throw new AlreadyAcceptedException(nameof(order), request.OrderId, order.AcceptedByUserId.Value);

            var user = _context.User.FirstOrDefault(x => x.Id == request.UserId);
            if (user == null) throw new NotFoundException(nameof(user), request.UserId);

            order.AcceptedByUserId = request.UserId;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}