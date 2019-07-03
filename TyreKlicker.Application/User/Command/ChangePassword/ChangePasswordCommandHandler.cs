//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using MediatR;
//using TyreKlicker.Application.Exceptions;
//using TyreKlicker.Persistence;

//namespace TyreKlicker.Application.User.Command.ChangePassword
//{
//    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand>
//    {
//        private readonly TyreKlickerDbContext _context;

//        public ChangePasswordCommandHandler(TyreKlickerDbContext context)
//        {
//            _context = context;
//        }

//        public Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
//        {
//            var user = _context.User.FirstOrDefault(x => x.Id == request.UserId);
//            if (user == null) throw new NotFoundException(nameof(user), request.UserId.ToString());

//            user.
//        }
//    }
//}
