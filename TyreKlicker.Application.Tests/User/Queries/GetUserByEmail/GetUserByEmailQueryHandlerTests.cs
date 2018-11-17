using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using TyreKlicker.Application.Exceptions;
using TyreKlicker.Application.Tests.Infrastructure;
using TyreKlicker.Application.User.Queries.GetUserByEmail;
using TyreKlicker.Persistence;
using Xunit;

namespace TyreKlicker.Application.Tests.User.Queries.GetUserByEmail
{
    [Collection("QueryCollection")]
    public class GetUserByEmailQueryHandlerTests
    {
        private readonly TyreKlickerDbContext _context;
        private readonly string _validUserEmail = "ryan@email.co.uk";
        private readonly string _invalidUserEmail = "iDoNotExists@InTheDatabase.co.uk";

        public GetUserByEmailQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async Task GetUserByEmail_UserExists_ReturnsUserViewModel()
        {
            var sut = new GetUserByEmailQueryHandler(_context);

            var result = await sut.Handle(new GetUserByEmailQuery { Email = _validUserEmail }, CancellationToken.None);

            result.ShouldBeOfType<UserViewModel>();
            result.Email.ShouldBe(_validUserEmail);
        }

        [Fact]
        public async Task GetUserByEmail_UserDoesNotExist_ThrowsNotFoundException()
        {
            var sut = new GetUserByEmailQueryHandler(_context);

            var result = await sut.Handle(new GetUserByEmailQuery { Email = _invalidUserEmail }, CancellationToken.None)

            .ShouldThrowAsync<NotFoundException>();
        }
    }
}