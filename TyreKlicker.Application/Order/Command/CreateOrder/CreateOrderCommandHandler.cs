﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TyreKlicker.Application.Interfaces;
using TyreKlicker.Persistence;

namespace TyreKlicker.Application.Order.Command.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Unit>
    {
        private readonly TyreKlickerDbContext _context;
        private readonly INotificationService _notificationService;

        public CreateOrderCommandHandler(
            TyreKlickerDbContext context,
            INotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
        }

        public async Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var entity = new TyreKlicker.Domain.Entities.Order
            {
                CreatedByUserId = request.CreatedByUserId,
                Description = request.Description,
                Registration = request.Registration,
            };

            _context.Order.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}