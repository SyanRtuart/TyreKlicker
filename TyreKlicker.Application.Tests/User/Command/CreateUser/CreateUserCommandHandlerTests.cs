using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Shouldly;
using TyreKlicker.Application.Tests.Infrastructure;
using TyreKlicker.Application.User.Command.CreateUser;
using TyreKlicker.Persistence;
using Xunit;

namespace TyreKlicker.Application.Tests.User.Command.CreateUser
{
    [Collection("QueryCollection")]
    public class CreateUserCommandHandlerTests
    {
        public CreateUserCommandHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
        }

        private readonly TyreKlickerDbContext _context;
        private readonly string _email = "rsrs@hot.co.uk";

        [Fact]
        public async Task CreateUserCommand_AfterModelHasBeenValidated_ShouldCreateUserAndReturnUserModel()
        {
            var sut = new CreateUserCommandHandler(_context);
            var userCountBeforeAct = _context.User.ToList().Count;

            await sut.Handle(new CreateUserCommand {Id = Guid.NewGuid(), Email = _email}, CancellationToken.None);
            var user = _context.User.FirstOrDefault(e => e.Email == _email);

            _context.User.ToList().Count.ShouldBe(userCountBeforeAct + 1);
            user.ShouldBeOfType<Domain.Entities.User>();
        }

        [Fact]
        public async Task CreateUserCommand_IdIsEmpty_ShouldThrowArgumentException()
        {
            var sut = new CreateUserCommandHandler(_context);

            await sut.Handle(new CreateUserCommand {Id = Guid.Empty}, CancellationToken.None)
                .ShouldThrowAsync<ArgumentException>();
        }
    }
}