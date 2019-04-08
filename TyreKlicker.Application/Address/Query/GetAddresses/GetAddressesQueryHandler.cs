using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TyreKlicker.Persistence;

namespace TyreKlicker.Application.Address.Query.GetAddresses
{
    public class GetAddressesQueryHandler : IRequestHandler<GetAddressesQuery, AddressListViewModel>
    {
        private readonly TyreKlickerDbContext _context;

        public GetAddressesQueryHandler(TyreKlickerDbContext context)
        {
            _context = context;
        }

        public async Task<AddressListViewModel> Handle(GetAddressesQuery request, CancellationToken cancellationToken)
        {
            var model = new AddressListViewModel
            {
                Addresses = await _context.Address
                    .Where(x => x.UserId == request.UserId)
                    .Select(AddressDto.Projection)
                    .OrderBy(a => !a.PrimaryAddress)
                    .ToListAsync(cancellationToken)
            };

            return model;
        }
    }
}