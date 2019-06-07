using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TyreKlicker.Persistence;

namespace TyreKlicker.Application.Payment.Command.CreatePayment
{
    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, Unit>
    {
        private readonly TyreKlickerDbContext _context;

        public CreatePaymentCommandHandler(TyreKlickerDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.Payment
            {
                Description = request.Description,
                Amount = request.Amount,
                Currency = request.Currency,
                ExternalPaymentId = request.ExternalPaymentId
            };

            await _context.Payment.AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}