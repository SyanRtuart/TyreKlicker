using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TyreKlicker.Application.Order.Queries.GetCurrentOrders;
using TyreKlicker.Application.Tests.Infrastructure;
using TyreKlicker.Persistence;
using Xunit;

namespace TyreKlicker.Application.Tests.Order.Queries.GetCurrentOrders
{
    [Collection("QueryCollection")]
    public class GetCurrentOrdersQueryHandlerTests : IClassFixture<GetCurrentOrdersFixture>
    {
        private readonly GetCurrentOrdersFixture _fixture;

        public GetCurrentOrdersQueryHandlerTests(GetCurrentOrdersFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetCurrentOrders_WhenCalled_ShouldReturnCountOfCurrentOrders()
        {
            var sut = new GetCurrentOrdersQueryHandler(_fixture.Context);

            var result = await sut.Handle(new GetCurrentOrdersQuery() { UserId = _fixture.CurrentUser.Id }, CancellationToken.None);
            var resultCount = result.Orders.Count();

            result.ShouldBeOfType<CurrentOrdersListViewModel>();
            resultCount.ShouldBe(2);
        }
    }

    [Collection("QueryCollection")]
    public class GetCurrentOrdersFixture : IDisposable
    {
        public TyreKlickerDbContext Context { get; set; }
        public List<Domain.Entities.Order> Orders { get; set; }
        public Domain.Entities.User CurrentUser { get; set; }

        public GetCurrentOrdersFixture(QueryTestFixture fixture)
        {
            Context = fixture.Context;

            CurrentUser = new Domain.Entities.User
            {
                Id = Guid.NewGuid()
            };
            Context.User.Add(CurrentUser);
            Orders = new List<Domain.Entities.Order>
            {
                new Domain.Entities.Order { Id = Guid.NewGuid(), Complete = false , AcceptedByUser = CurrentUser},
                new Domain.Entities.Order { Id = Guid.NewGuid(), Complete = false, AcceptedByUser = CurrentUser},
                new Domain.Entities.Order { Id = Guid.NewGuid(), Complete = true}
            };
            Context.Order.AddRange(Orders);
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Order.RemoveRange(Orders);
            Context.User.Remove(CurrentUser);
        }
    }
}