using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Shouldly;
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
        public GetUserByEmailQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
        }

        private readonly TyreKlickerDbContext _context;

        [Fact]
        public async Task GetUserByEmail_UserDoesNotExist_ThrowsNotFoundException()
        {
            var sut = new GetUserByEmailQueryHandler(_context);

            await sut.Handle(new GetUserByEmailQuery {Email = "iDoNotExists@InTheDatabase.co.uk"},
                    CancellationToken.None)
                .ShouldThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task GetUserByEmail_UserExists_ReturnsUserViewModel()
        {
            var sut = new GetUserByEmailQueryHandler(_context);
            var user = _context.User.First();

            var result = await sut.Handle(new GetUserByEmailQuery {Email = user.Email}, CancellationToken.None);

            result.ShouldBeOfType<UserViewModel>();
            result.Email.ShouldBe(user.Email);
        }
    }
}