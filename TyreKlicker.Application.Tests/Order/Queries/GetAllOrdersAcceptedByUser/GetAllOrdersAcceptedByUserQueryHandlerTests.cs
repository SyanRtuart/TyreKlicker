using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TyreKlicker.Application.Order.Queries.GetAllOrdersAcceptedByUser;
using TyreKlicker.Application.Tests.Infrastructure;
using TyreKlicker.Persistence;
using Xunit;

namespace TyreKlicker.Application.Tests.Order.Queries.GetAllOrdersAcceptedByUser
{
    [Collection("QueryCollection")]
    public class GetAllOrdersAcceptedByUserQueryHandlerTests
    {
        //ToDo Fix this unit test
        private readonly TyreKlickerDbContext _context;

        private static readonly Guid AcceptedByUserIdToTest = Guid.Parse("1114e3c0-093f-4e18-be42-d7c3e178a22c");

        public GetAllOrdersAcceptedByUserQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async Task GetAllOrdersAcceptedByUser_WhenCalled_ShouldReturnOrderAcceptedByUserListViewModel()
        {
            var sut = new GetAllOrdersAcceptedByUserQueryHandler(_context);

            var result = await sut.Handle(new GetAllOrdersAcceptedByUserQuery(), CancellationToken.None);

            result.ShouldBeOfType<OrderAcceptedByUserListViewModel>();
        }

        [Theory, MemberData(nameof(TestGuids))]
        public async Task GetAllOrdersAcceptedByUser_WhenCalled_ShouldReturnCountOfOrdersAcceptedByGivenUser(Guid userGuid2, Guid userGuid3)
        {
            var sut = new GetAllOrdersAcceptedByUserQueryHandler(_context);
            var orders = new List<Domain.Entities.Order>()
            {
                new Domain.Entities.Order { AcceptedByUserId = AcceptedByUserIdToTest},
                new Domain.Entities.Order { AcceptedByUserId = userGuid2},
                new Domain.Entities.Order { AcceptedByUserId = userGuid3}
            };
            _context.Order.AddRange(orders);
            _context.SaveChanges();
            var acceptedOrdersInDb = _context.Order.Count(x => x.AcceptedByUserId == AcceptedByUserIdToTest);

            var result = await sut.Handle(new GetAllOrdersAcceptedByUserQuery() { UserId = AcceptedByUserIdToTest }, CancellationToken.None);
            var resultCount = result.Orders.Count();

            resultCount.ShouldBe(acceptedOrdersInDb);
        }

        public static IEnumerable<object[]> TestGuids
        {
            get
            {
                yield return new object[] { Guid.NewGuid(), Guid.NewGuid() };
                yield return new object[] { AcceptedByUserIdToTest, Guid.NewGuid() };
                yield return new object[] { AcceptedByUserIdToTest, AcceptedByUserIdToTest };
            }
        }
    }
}