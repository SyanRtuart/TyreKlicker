using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TyreKlicker.Application.Address.Command.SetPrimaryAddress;
using TyreKlicker.Application.Tests.Infrastructure;
using TyreKlicker.Persistence;
using Xunit;

namespace TyreKlicker.Application.Tests.Address.Command.SetPrimaryAddress
{
    [Collection("QueryCollection")]
    public class SetPrimaryAddressCommandHandlerTests : IClassFixture<SetPrimaryAddressCommandHandlerFixture>
    {
        private readonly SetPrimaryAddressCommandHandlerFixture _fixture;

        public SetPrimaryAddressCommandHandlerTests(SetPrimaryAddressCommandHandlerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task SetPrimaryAddress_WhenCalled_ShouldSetAddressAsPrimary()
        {
            var sut = new SetPrimaryAddressCommandHandler(_fixture.Context);

            await sut.Handle(new SetPrimaryAddressCommand()
            {
                Id = _fixture.Addresses.First().Id,
                IsPrimary = true
            }, CancellationToken.None);

            _fixture.Addresses.First().PrimaryAddress.ShouldBe(true);
        }
    }

    [Collection("QueryCollection")]
    public class SetPrimaryAddressCommandHandlerFixture
    {
        public TyreKlickerDbContext Context { get; set; }
        public List<Domain.Entities.Address> Addresses { get; set; }
        public Domain.Entities.User CurrentUser { get; set; }

        public SetPrimaryAddressCommandHandlerFixture(QueryTestFixture fixture)
        {
            Context = fixture.Context;

            CurrentUser = new Domain.Entities.User { Id = Guid.NewGuid() };
            Context.User.Add(CurrentUser);

            Addresses = new List<Domain.Entities.Address>
            {
                new Domain.Entities.Address{ UserId = CurrentUser.Id },
                new Domain.Entities.Address{ UserId = CurrentUser.Id },
                new Domain.Entities.Address{ UserId = Guid.NewGuid()},
            };

            Context.Address.AddRange(Addresses);

            Context.SaveChanges();
        }
    }
}