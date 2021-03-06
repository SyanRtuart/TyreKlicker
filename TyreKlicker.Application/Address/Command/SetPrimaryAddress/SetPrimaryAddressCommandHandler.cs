﻿using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TyreKlicker.Application.Exceptions;
using TyreKlicker.Persistence;

namespace TyreKlicker.Application.Address.Command.SetPrimaryAddress
{
    public class SetPrimaryAddressCommandHandler : IRequestHandler<SetPrimaryAddressCommand, Unit>
    {
        private readonly TyreKlickerDbContext _context;

        public SetPrimaryAddressCommandHandler(TyreKlickerDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(SetPrimaryAddressCommand request, CancellationToken cancellationToken)
        {
            var address = _context.Address.FirstOrDefault(x =>
                x.Id == request.Id &&
                x.UserId == request.UserId);

            if (address == null) throw new NotFoundException(nameof(address), request.Id.ToString());

            var currentPrimaryAddress = _context.Address.Where(x =>
                    x.UserId == request.UserId &&
                    x.PrimaryAddress)
                .ToList();

            currentPrimaryAddress.ForEach(x => x.PrimaryAddress = false);

            address.PrimaryAddress = request.IsPrimary;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}