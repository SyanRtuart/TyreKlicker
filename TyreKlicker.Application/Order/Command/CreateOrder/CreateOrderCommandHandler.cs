﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TyreKlicker.Application.Exceptions;
using TyreKlicker.Domain.Entities;
using TyreKlicker.Persistence;

namespace TyreKlicker.Application.Order.Command.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Unit>
    {
        private readonly TyreKlickerDbContext _context;
        private readonly IMapper _mapper;

        public CreateOrderCommandHandler(TyreKlickerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Id == request.CreatedByUserId, cancellationToken);

            if (user == null) throw new NotFoundException(nameof(user), request.CreatedByUserId.ToString());

            var address = await _context.Address.FirstOrDefaultAsync(a => a.Id == request.AddressId, cancellationToken);

            if (address == null) throw new NotFoundException(nameof(address), request.AddressId.ToString());

            var entity = new Domain.Entities.Order
            {
                Address = address,
                CreatedBy = request.CreatedByUserId,
                Description = request.Description,
                Registration = request.Registration,
                Make = request.Make,
                Model = request.Model,
                Trim = request.Trim,
                Tyre = request.Tyre,
                Year = request.Year,
                Availability = _mapper.Map<IEnumerable<Availability>>(request.Availability)
            };

            _context.Order.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}