using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TyreKlicker.Application.Address.Command.SetPrimaryAddress;
using TyreKlicker.Application.Exceptions;
using TyreKlicker.Application.Tests.Infrastructure;
using TyreKlicker.Persistence;
using Xunit;

namespace TyreKlicker.Application.Tests.Address.Command.SetPrimaryAddress
{
    [Collection("SetPrimaryAddressCommandHandlerFixture")]
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
                UserId = _fixture.CurrentUserId,
                IsPrimary = true
            }, CancellationToken.None);

            _fixture.Addresses.First().PrimaryAddress.ShouldBe(true);
        }

        [Fact]
        public async Task SetPrimaryAddress_AddressDoesNotExist_ShouldThrowNotFoundException()
        {
            var sut = new SetPrimaryAddressCommandHandler(_fixture.Context);

            await sut.Handle(new SetPrimaryAddressCommand()
            {
                Id = Guid.NewGuid(),
                IsPrimary = true
            }, CancellationToken.None)

            .ShouldThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task SetPrimaryAddress_AnotherAddressIsAlreadyPrimary_ShouldSetNewAddressAsPrimaryOnly()
        {
            var sut = new SetPrimaryAddressCommandHandler(_fixture.Context);
            var newPrimaryAddress = _fixture.Context.Address.Last();

            await sut.Handle(new SetPrimaryAddressCommand()
            {
                Id = newPrimaryAddress.Id,
                UserId = _fixture.CurrentUserId,
                IsPrimary = true
            }, CancellationToken.None);

            newPrimaryAddress.PrimaryAddress.ShouldBe(true);
            _fixture.CurrentPrimaryAddress.PrimaryAddress.ShouldBe(false);
        }
    }

    [Collection("SetPrimaryAddressCommandHandlerFixture")]
    public class SetPrimaryAddressCommandHandlerFixture : IDisposable
    {
        public TyreKlickerDbContext Context { get; set; }
        public List<Domain.Entities.Address> Addresses { get; set; }
        public Guid CurrentUserId { get; set; }
        public Domain.Entities.Address CurrentPrimaryAddress { get; set; }

        public SetPrimaryAddressCommandHandlerFixture()
        {
            Context = TyreKlickerContextFactory.Create();

            CurrentUserId = Guid.NewGuid();
            CurrentPrimaryAddress = new Domain.Entities.Address { UserId = CurrentUserId, PrimaryAddress = true };

            Addresses = new List<Domain.Entities.Address>
            {
                new Domain.Entities.Address{ UserId =  CurrentUserId},
                new Domain.Entities.Address{ UserId =  CurrentUserId},
                new Domain.Entities.Address{ UserId =  CurrentUserId},
            };
            Context.Address.Add(CurrentPrimaryAddress);
            Context.Address.AddRange(Addresses);

            Context.SaveChanges();
        }

        public void Dispose()
        {
            TyreKlickerContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("SetPrimaryAddressCommandHandlerCollection")]
    public class SetPrimaryAddressCommandHandlerCollection : ICollectionFixture<SetPrimaryAddressCommandHandlerFixture> { }
}