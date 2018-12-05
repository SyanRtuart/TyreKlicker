using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Utilities;
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
                .SingleOrDefaultAsync(o => o.OrderId == request.OrderId, cancellationToken);

            if (order == null) throw new NotFoundException(nameof(order), request.OrderId);

            order.Result.Complete = request.Complete;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
