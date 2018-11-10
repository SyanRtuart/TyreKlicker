namespace TyreKlicker.Application.ApplicationUser.Command.Register
{
    //public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Unit>
    //{
    //    private readonly TyreKlickerDbContext _context;
    //    private readonly INotificationService _notificationService;

    //    public RegisterUserCommandHandler(
    //        TyreKlickerDbContext context,
    //        INotificationService notificationService)
    //    {
    //        _context = context;
    //        _notificationService = notificationService;
    //    }

    //    public Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    //    {
    //        var entity = new Domain.Entities.User
    //        {
    //            Email = request.Email,
    //            FirstName = request.FirstName,
    //            LastName = request.LastName,
    //            Password = request.Password
    //        };

    //        _context.User.Add(entity);

    //        await _context.SaveChangesAsync(cancellationToken);

    //        return Unit.Value;
    //    }
    //}
}