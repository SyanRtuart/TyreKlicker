using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TyreKlicker.Application.Exceptions;
using TyreKlicker.Application.Order.Models;
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
            var order = await _context.Order.Where(x => x.Id == request.Id)
                .Include(availability => availability.Availability)
                .SingleOrDefaultAsync(cancellationToken);

            if (order == null) throw new NotFoundException(nameof(order), request.Id.ToString());

            return _mapper.Map<OrderDto>(order);
        }
    }
}