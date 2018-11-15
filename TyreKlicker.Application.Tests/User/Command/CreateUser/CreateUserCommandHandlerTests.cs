using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TyreKlicker.Application.Tests.Infrastructure;
using TyreKlicker.Application.User.Command.CreateUser;
using TyreKlicker.Persistence;
using Xunit;

namespace TyreKlicker.Application.Tests.User.Command.CreateUser
{
    [Collection("QueryCollection")]

    public class CreateUserCommandHandlerTests
    {
        private readonly TyreKlickerDbContext _context;
        private readonly string _email = "rsrs@hot.co.uk";
        //private readonly string _validUserEmail = "ryan@email.co.uk";

        public CreateUserCommandHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async Task CreateUserCommand_UserIsCreated_ReturnsUserModel()
        {
            var sut = new CreateUserCommandHandler(_context);

            await sut.Handle(new CreateUserCommand
            {
                FirstName="srs",
                LastName = "steven",
                PhoneNumber ="4855" ,
                Email = _email
            }, CancellationToken.None);
            var user = _context.User.FirstOrDefault(e => e.Email == _email);
            

            user.ShouldBeOfType<Domain.Entities.User>();
        }

        [Fact]
        public async Task CreateUserCommand_UserIsNotCreated_ReturnsUserModel()
        {
            var sut = new CreateUserCommandHandler(_context);

            await sut.Handle(new CreateUserCommand
            {
                FirstName = "",
                LastName = "",
                PhoneNumber = "",
                Email = ""
            }, CancellationToken.None);
            var user = _context.User.FirstOrDefault(e => e.Email == _email);


            user.ShouldBeNull();
        }
    }
}
