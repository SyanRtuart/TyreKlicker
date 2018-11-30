using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            var entity = _context.Order.FirstOrDefault(x => x.OrderId == request.OrderId);
            var user = _context.User.FirstOrDefault(x => x.Id == request.UserId);


            if (entity == null) throw new NotFoundException(nameof(entity), request.OrderId);

            if (user == null) throw new NotFoundException(nameof(user), request.UserId);

            entity.AcceptedByUserId = request.UserId;

            _context.SaveChanges();

            return Unit.Value;
        }
    }
}
