using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TyreKlicker.Application.Address.Query.GetAddresses;
using TyreKlicker.Application.Tests.Infrastructure;
using TyreKlicker.Persistence;
using Xunit;

namespace TyreKlicker.Application.Tests.Address.Queries.GetAddresses
{
    [Collection("QueryCollection")]
    public class GetAddressesQueryHandlerTests : IClassFixture<GetAddressesQueryHandlerFixture>
    {
        private readonly GetAddressesQueryHandlerFixture _fixture;

        public GetAddressesQueryHandlerTests(GetAddressesQueryHandlerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetAddresses_ExistingUserId_ShouldReturnAddresses()
        {
            var sut = new GetAddressesQueryHandler(_fixture.Context);

            var result = await sut.Handle(new GetAddressesQuery() { UserId = _fixture.CurrentUser.Id }, CancellationToken.None);
            var resultCount = result.Addresses.Count();

            result.ShouldBeOfType<AddressListViewModel>();
            resultCount.ShouldBe(2);
        }

        [Fact]
        public async Task GetAddresses_FakeUserId_ShouldReturnCountOfZero()
        {
            var sut = new GetAddressesQueryHandler(_fixture.Context);

            var result = await sut.Handle(new GetAddressesQuery() { UserId = Guid.NewGuid() }, CancellationToken.None);
            var resultCount = result.Addresses.Count();

            result.ShouldBeOfType<AddressListViewModel>();
            resultCount.ShouldBe(0);
        }
    }

    [Collection("QueryCollection")]
    public class GetAddressesQueryHandlerFixture : IDisposable
    {
        public TyreKlickerDbContext Context { get; set; }
        public List<Domain.Entities.Address> Addresses { get; set; }
        public Domain.Entities.User CurrentUser { get; set; }

        public GetAddressesQueryHandlerFixture(QueryTestFixture fixture)
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

        public void Dispose()
        {
            Context.Address.RemoveRange(Addresses);
            Context.User.Remove(CurrentUser);
        }
    }
}