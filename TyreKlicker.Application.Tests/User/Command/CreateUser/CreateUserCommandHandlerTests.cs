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

        public CreateUserCommandHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async Task CreateUserCommand_AfterModelHasBeenValidated_ReturnsUserModel()
        {
            var sut = new CreateUserCommandHandler(_context);
            var userCountBeforeAct = _context.User.ToList().Count;

            await sut.Handle(new CreateUserCommand { Email = _email }, CancellationToken.None);
            var user = _context.User.FirstOrDefault(e => e.Email == _email);

            _context.User.ToList().Count.ShouldBe(userCountBeforeAct + 1);
            user.ShouldBeOfType<Domain.Entities.User>();
        }
    }
}