using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TyreKlicker.Application.Exceptions;
using TyreKlicker.Persistence;

namespace TyreKlicker.Application.Address.Query.GetPrimaryAddress
{
    public class GetPrimaryAddressQueryHandler : IRequestHandler<GetPrimaryAddressQuery, AddressDto>
    {
        private readonly TyreKlickerDbContext _context;

        public GetPrimaryAddressQueryHandler(TyreKlickerDbContext context)
        {
            _context = context;
        }

        public async Task<AddressDto> Handle(GetPrimaryAddressQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken: cancellationToken);

            if (user == null) throw new NotFoundException(nameof(user), request.UserId.ToString());

            var addressInDb = await _context.Address
                .Where(a =>
                    a.UserId == request.UserId &&
                    a.PrimaryAddress)
                .Select(AddressDto.Projection)
                .FirstOrDefaultAsync(cancellationToken);

            if (addressInDb == null) throw new NotFoundException(nameof(addressInDb), request.UserId.ToString());

            return addressInDb;
        }
    }
}