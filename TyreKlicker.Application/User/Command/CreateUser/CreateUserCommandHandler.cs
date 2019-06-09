using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TyreKlicker.Persistence;

namespace TyreKlicker.Application.User.Command.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Unit>
    {
        private readonly TyreKlickerDbContext _context;

        public CreateUserCommandHandler(TyreKlickerDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == null || request.Id == Guid.Empty) throw new ArgumentException("ID cannot be null");

            var entity = new Domain.Entities.User
            {
                Id = request.Id,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            _context.User.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}