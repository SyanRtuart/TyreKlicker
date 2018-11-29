using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TyreKlicker.Application.Exceptions;
using TyreKlicker.Persistence;

namespace TyreKlicker.Application.User.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserViewModel>
    {
        public readonly TyreKlickerDbContext _context;

        public GetUserByIdQueryHandler(TyreKlickerDbContext context)
        {
            _context = context;
        }

        public async Task<UserViewModel> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.User
                .Where(u => u.Id == request.Id)
                .Select(UserViewModel.Projection)
                .SingleOrDefaultAsync(cancellationToken);

            if (user == null)
            {
                throw new NotFoundException(nameof(user), request.Id);
            }

            return user;
        }
    }
}