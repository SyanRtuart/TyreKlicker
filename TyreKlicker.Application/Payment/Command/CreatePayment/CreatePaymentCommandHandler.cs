using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TyreKlicker.Application.Interfaces;
using TyreKlicker.Application.Payment.Models;
using TyreKlicker.Persistence;

namespace TyreKlicker.Application.Payment.Command.CreatePayment
{
    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, Unit>
    {
        private readonly TyreKlickerDbContext _context;
        private readonly IPaymentService _paymentService;

        public CreatePaymentCommandHandler(TyreKlickerDbContext context, IPaymentService paymentService)
        {
            _context = context;
            _paymentService = paymentService;
        }

        public async Task<Unit> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var paymentId = await _paymentService.MakePayment(new PaymentDto
            {
                Description = request.Description,
                Currency = request.Currency,
                Email = request.Email,
                Amount = request.Amount,
                Token = request.Token
            }, _paymentService.SecretKey);


            var entity = new Domain.Entities.Payment
            {
                Description = request.Description,
                Amount = request.Amount,
                Currency = request.Currency,
                ExternalPaymentId = paymentId
            };

            await _context.Payment.AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}