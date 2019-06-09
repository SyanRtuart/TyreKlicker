using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Shouldly;
using TyreKlicker.Application.Exceptions;
using TyreKlicker.Application.Tests.Infrastructure;
using TyreKlicker.Application.User.Queries.GetUserById;
using TyreKlicker.Persistence;
using Xunit;

namespace TyreKlicker.Application.Tests.User.Queries.GetUserById
{
    [Collection("QueryCollection")]
    public class GetUserByIdQueryHandlerTests
    {
        //private readonly Guid _userInDb = Guid.Parse("2220d661-6a96-4537-a896-5014374d39f5");

        public GetUserByIdQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
        }

        private readonly TyreKlickerDbContext _context;

        [Fact]
        public async Task GetUserById_UserDoesNotExist_ThrowsNotFoundException()
        {
            var sut = new GetUserByIdQueryHandler(_context);

            await sut.Handle(new GetUserByIdQuery {Id = Guid.NewGuid()}, CancellationToken.None)
                .ShouldThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task GetUserById_UserExists_ReturnsUserViewModel()
        {
            var sut = new GetUserByIdQueryHandler(_context);
            var user = _context.User.First();

            var result = await sut.Handle(new GetUserByIdQuery {Id = user.Id}, CancellationToken.None);

            result.ShouldBeOfType<UserViewModel>();
            result.Id.ShouldBe(user.Id);
        }
    }
}