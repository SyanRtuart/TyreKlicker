using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TyreKlicker.Application.Exceptions;
using TyreKlicker.Persistence;

namespace TyreKlicker.Application.Address.Command.CreateAddress
{
    public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, Unit>
    {
        private readonly TyreKlickerDbContext _context;

        public CreateAddressCommandHandler(TyreKlickerDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.User.FirstOrDefaultAsync(u =>
                u.Id == request.UserId, cancellationToken);

            if (user == null) throw new NotFoundException(nameof(user), request.UserId.ToString());

            var entity = new Domain.Entities.Address
            {
                UserId = request.UserId,
                City = request.City,
                PhoneNumber = request.PhoneNumber,
                Postcode = request.Postcode,
                Street = request.Street,
                PrimaryAddress = request.PrimaryAddress,
            };

            _context.Address.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}