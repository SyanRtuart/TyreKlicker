using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Shouldly;
using TyreKlicker.Application.Address.Query.GetPrimaryAddress;
using TyreKlicker.Application.Exceptions;
using TyreKlicker.Application.Tests.Infrastructure;
using TyreKlicker.Persistence;
using Xunit;

namespace TyreKlicker.Application.Tests.Address.Queries.GetPrimaryAddress
{
    [Collection("GetPrimaryAddressQueryHandlerCollection")]
    public class GetPrimaryAddressQueryHandlerTests : IClassFixture<GetPrimaryAddressQueryHandlerFixture>
    {
        public GetPrimaryAddressQueryHandlerTests(GetPrimaryAddressQueryHandlerFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly GetPrimaryAddressQueryHandlerFixture _fixture;

        [Fact]
        public async Task GetPrimaryAddress_NoAddressForUser_ShouldThrowNotFoundException()
        {
            var sut = new GetPrimaryAddressQueryHandler(_fixture.Context);

            await sut.Handle(new GetPrimaryAddressQuery {UserId = _fixture.UserWithNoAddress.Id},
                CancellationToken.None).ShouldThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task GetPrimaryAddress_UserDoesNotExist_ShouldThrowNotFoundException()
        {
            var sut = new GetPrimaryAddressQueryHandler(_fixture.Context);

            await sut.Handle(new GetPrimaryAddressQuery {UserId = Guid.NewGuid()}, CancellationToken.None)
                .ShouldThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task GetPrimaryAddress_WhenCalled_ShouldReturnPrimaryAddress()
        {
            var sut = new GetPrimaryAddressQueryHandler(_fixture.Context);

            var result = await sut.Handle(new GetPrimaryAddressQuery {UserId = _fixture.CurrentUser.Id},
                CancellationToken.None);

            result.PrimaryAddress.ShouldBe(true);
        }
    }

    [Collection("GetPrimaryAddressQueryHandlerCollection")]
    public class GetPrimaryAddressQueryHandlerFixture : IDisposable
    {
        public GetPrimaryAddressQueryHandlerFixture()
        {
            Context = TyreKlickerContextFactory.Create();

            CurrentUser = new Domain.Entities.User();
            UserWithNoAddress = new Domain.Entities.User();
            Context.Add(CurrentUser);
            Context.Add(UserWithNoAddress);

            CurrentPrimaryAddress = new Domain.Entities.Address {UserId = CurrentUser.Id, PrimaryAddress = true};

            Addresses = new List<Domain.Entities.Address>
            {
                new Domain.Entities.Address {UserId = CurrentUser.Id, PrimaryAddress = true},
                new Domain.Entities.Address {UserId = CurrentUser.Id}
            };
            Context.Address.Add(CurrentPrimaryAddress);
            Context.Address.AddRange(Addresses);

            Context.SaveChanges();
        }

        public TyreKlickerDbContext Context { get; set; }
        public List<Domain.Entities.Address> Addresses { get; set; }
        public Domain.Entities.User CurrentUser { get; set; }
        public Domain.Entities.User UserWithNoAddress { get; set; }
        public Domain.Entities.Address CurrentPrimaryAddress { get; set; }

        public void Dispose()
        {
        }
    }

    [CollectionDefinition("GetPrimaryAddressQueryHandlerCollection")]
    public class GetPrimaryAddressQueryHandlerCollection : ICollectionFixture<GetPrimaryAddressQueryHandlerFixture>
    {
    }
}