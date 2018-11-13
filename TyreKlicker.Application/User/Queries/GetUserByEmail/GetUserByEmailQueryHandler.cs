using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TyreKlicker.Application.Exceptions;
using TyreKlicker.Persistence;

namespace TyreKlicker.Application.User.Queries.GetUserByAccountId
{
    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, UserViewModel>
    {
        private readonly TyreKlickerDbContext _context;

        public GetUserByEmailQueryHandler(TyreKlickerDbContext context)
        {
            _context = context;
        }

        public async Task<UserViewModel> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.User
                .Where(u => u.Email == request.Email)
                .Select(UserViewModel.Projection)
                .SingleOrDefaultAsync(cancellationToken);

            if (user == null)
            {
                throw new NotFoundException(nameof(user), request.Email);
            }

            return user;
        }
    }
}