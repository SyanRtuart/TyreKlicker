using Shouldly;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TyreKlicker.Application.Address.Command.CreateAddress;
using TyreKlicker.Application.Exceptions;
using TyreKlicker.Application.Tests.Infrastructure;
using TyreKlicker.Persistence;
using Xunit;

namespace TyreKlicker.Application.Tests.Address.Command.CreateAddress
{
    [Collection("QueryCollection")]
    public class CreateAddressCommandHandlerTests : IClassFixture<CreateAddressCommandHandlerFixture>
    {
        private readonly CreateAddressCommandHandlerFixture _fixture;

        public CreateAddressCommandHandlerTests(CreateAddressCommandHandlerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task CreateAddress_WhenCalled_ShouldCreateAddress()
        {
            var sut = new CreateAddressCommandHandler(_fixture.Context);
            var addressCountBeforeAct = _fixture.Context.Address.ToList().Count;

            await sut.Handle(new CreateAddressCommand { UserId = _fixture.CurrentUser.Id }, CancellationToken.None);
            var addressCountAfterAct = _fixture.Context.Address.ToList().Count;

            addressCountAfterAct.ShouldBe(addressCountBeforeAct + 1);
        }

        [Fact]
        public async Task CreateAddress_UserDoesNotExist_ShouldThrowException()
        {
            var sut = new CreateAddressCommandHandler(_fixture.Context);

            await sut.Handle(new CreateAddressCommand { UserId = Guid.NewGuid() }, CancellationToken.None)

            .ShouldThrowAsync<NotFoundException>();
        }
    }

    [Collection("QueryCollection")]
    public class CreateAddressCommandHandlerFixture
    {
        public TyreKlickerDbContext Context { get; set; }
        public Domain.Entities.User CurrentUser { get; set; }

        public CreateAddressCommandHandlerFixture(QueryTestFixture fixture)
        {
            Context = fixture.Context;
            CurrentUser = new Domain.Entities.User();
            Context.Add(CurrentUser);
            Context.SaveChanges();
        }
    }
}