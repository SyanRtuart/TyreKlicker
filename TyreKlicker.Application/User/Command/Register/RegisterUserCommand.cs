using MediatR;

namespace TyreKlicker.Application.ApplicationUser.Command.Register
{
    public class RegisterUserCommand : IRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}