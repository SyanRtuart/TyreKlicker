using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Shouldly;
using TyreKlicker.Application.Address.Query.GetAddress;
using TyreKlicker.Application.Exceptions;
using TyreKlicker.Application.Tests.Infrastructure;
using TyreKlicker.Persistence;
using Xunit;

namespace TyreKlicker.Application.Tests.Address.Queries.GetAddress
{
    [Collection("QueryCollection")]
    public class GetAddressQueryHandlerTests : IClassFixture<GetAddressQueryHandlerFixture>
    {
        public GetAddressQueryHandlerTests(GetAddressQueryHandlerFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly GetAddressQueryHandlerFixture _fixture;

        [Fact]
        public async Task GetAddress_InvalidId_ShouldReturnAddress()
        {
            var sut = new GetAddressQueryHandler(_fixture.Context);

            await sut.Handle(new GetAddressQuery {Id = Guid.NewGuid()}, CancellationToken.None)
                .ShouldThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task GetAddress_ValidId_ShouldReturnAddress()
        {
            var sut = new GetAddressQueryHandler(_fixture.Context);

            var result = await sut.Handle(new GetAddressQuery {Id = _fixture.ValidAddressId}, CancellationToken.None);

            result.ShouldBeOfType<AddressDto>();
            result.Id.ShouldBe(_fixture.ValidAddressId);
        }
    }

    public class GetAddressQueryHandlerFixture : IDisposable
    {
        public GetAddressQueryHandlerFixture(QueryTestFixture fixture)
        {
            Context = fixture.Context;
            ValidAddressId = Guid.NewGuid();

            Addresses = new List<Domain.Entities.Address>
            {
                new Domain.Entities.Address {Id = ValidAddressId},
                new Domain.Entities.Address {Id = Guid.NewGuid()},
                new Domain.Entities.Address {Id = Guid.NewGuid()}
            };

            Context.Address.AddRange(Addresses);

            Context.SaveChanges();
        }

        public TyreKlickerDbContext Context { get; set; }
        public List<Domain.Entities.Address> Addresses { get; set; }
        public Guid ValidAddressId { get; set; }

        public void Dispose()
        {
            Context.Address.RemoveRange(Addresses);
        }
    }
}