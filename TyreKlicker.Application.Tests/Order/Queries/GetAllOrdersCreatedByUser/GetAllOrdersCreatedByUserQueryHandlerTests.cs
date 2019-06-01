using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TyreKlicker.Application.Order.Queries.GetAllOrdersCreatedByUser;
using TyreKlicker.Application.Tests.Infrastructure;
using TyreKlicker.Persistence;
using Xunit;

namespace TyreKlicker.Application.Tests.Order.Queries.GetAllOrdersCreatedByUser
{
    [Collection("QueryCollection")]
    public class GetAllOrdersCreatedByUserQueryHandlerTests
    {
        //ToDo Fix this unit test
        private readonly TyreKlickerDbContext _context;

        private static readonly Guid CreatedByUserIdToTest = Guid.Parse("2220d661-6a96-4537-a896-5014374d39f5");

        public GetAllOrdersCreatedByUserQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async Task GetAllOrdersCreatedByUser_WhenCalled_ShouldReturnOrderListViewModel()
        {
            var sut = new GetAllOrdersCreatedUserQueryHandler(_context);

            var result = await sut.Handle(new GetAllOrdersCreatedByUserQuery(), CancellationToken.None);

            result.ShouldBeOfType<OrderCreatedByUserListViewModel>();
        }

        [Theory, MemberData(nameof(TestGuids))]
        public async Task GetAllOrdersAcceptedByUser_WhenCalled_ShouldReturnCountOfOrdersAcceptedByGivenUser(Guid userGuid1, Guid userGuid2)
        {
            var sut = new GetAllOrdersCreatedUserQueryHandler(_context);
            var orders = new List<Domain.Entities.Order>()
            {
                new Domain.Entities.Order { CreatedBy = CreatedByUserIdToTest},
                new Domain.Entities.Order { CreatedBy = userGuid1},
                new Domain.Entities.Order { CreatedBy = userGuid2}
            };
            _context.Order.AddRange(orders);
            _context.SaveChanges();
            var createdOrdersInDb = _context.Order.Count(x => x.CreatedBy == CreatedByUserIdToTest);

            var result = await sut.Handle(new GetAllOrdersCreatedByUserQuery() { UserId = CreatedByUserIdToTest }, CancellationToken.None);
            var resultCount = result.Orders.Count();

            resultCount.ShouldBe(createdOrdersInDb);
        }

        public static IEnumerable<object[]> TestGuids
        {
            get
            {
                yield return new object[] { Guid.NewGuid(), Guid.NewGuid() };
                yield return new object[] { CreatedByUserIdToTest, Guid.NewGuid() };
                yield return new object[] { CreatedByUserIdToTest, CreatedByUserIdToTest };
            }
        }
    }
}