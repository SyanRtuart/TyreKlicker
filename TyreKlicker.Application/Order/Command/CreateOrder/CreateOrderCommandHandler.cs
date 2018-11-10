using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TyreKlicker.Application.Interfaces;
using TyreKlicker.Persistence;

namespace TyreKlicker.Application.Job.Command.CreateJob
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
                Description = request.Description,
                Registration = request.Registration,
            };

            _context.Order.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
            //throw new NotImplementedException();
        }
    }
}