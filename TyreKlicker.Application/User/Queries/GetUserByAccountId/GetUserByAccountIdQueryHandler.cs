using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TyreKlicker.Persistence;

namespace TyreKlicker.Application.User.Queries.GetUserByAccountId
{
    public class GetUserByAccountIdQueryHandler : IRequestHandler<GetUserByAccountIdQuery, UserViewModel>
    {
        private readonly TyreKlickerDbContext _context;

        public GetUserByAccountIdQueryHandler(TyreKlickerDbContext context)
        {
            _context = context;
        }

        public async Task<UserViewModel> Handle(GetUserByAccountIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.User
                .Where(u => u.AccountId == request.Id)
                .Select(UserViewModel.Projection)
                .SingleOrDefaultAsync(cancellationToken);

            //TODO Add exceptions
            //if (user == null)
            //{
            //    throw new NotFoundException(nameof(user), request.Id);
            //}

            return user;
        }
    }
}