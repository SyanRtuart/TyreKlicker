using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TyreKlicker.Application.Exceptions;
using TyreKlicker.Persistence;

namespace TyreKlicker.Application.Address.Query.GetAddress
{
    public class GetAddressQueryHandler : IRequestHandler<GetAddressQuery, AddressDto>
    {
        private readonly TyreKlickerDbContext _context;

        public GetAddressQueryHandler(TyreKlickerDbContext context)
        {
            _context = context;
        }

        public async Task<AddressDto> Handle(GetAddressQuery request, CancellationToken cancellationToken)
        {
            var addressInDb = await _context.Address
                .Where(a => a.Id == request.Id)
                .Select(AddressDto.Projection)
                .FirstOrDefaultAsync(cancellationToken);

            if (addressInDb == null) throw new NotFoundException(nameof(addressInDb), request.Id.ToString());

            return addressInDb;
        }
    }
}