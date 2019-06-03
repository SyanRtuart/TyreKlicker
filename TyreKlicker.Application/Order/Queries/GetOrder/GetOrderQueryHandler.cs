using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TyreKlicker.Persistence;

namespace TyreKlicker.Application.Order.Queries.GetOrder
{
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderDto>
    {
        private readonly TyreKlickerDbContext _context;
        private readonly IMapper _mapper;

        public GetOrderQueryHandler(TyreKlickerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OrderDto> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<OrderDto>(await _context.Order.Where(x => x.Id == request.Id)
                .Include(availability => availability.Availability)
                .SingleOrDefaultAsync(cancellationToken));

            return entity;
        }
    }
}