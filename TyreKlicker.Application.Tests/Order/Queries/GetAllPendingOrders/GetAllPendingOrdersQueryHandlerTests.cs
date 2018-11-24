using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TyreKlicker.Application.Order.Queries.GetAllPendingOrders;
using TyreKlicker.Application.Tests.Infrastructure;
using TyreKlicker.Persistence;
using Xunit;

namespace TyreKlicker.Application.Tests.Order.Queries.GetAllPendingOrders
{
    [Collection("QueryCollection")]
    public class GetAllPendingOrdersQueryHandlerTests
    {
        private readonly TyreKlickerDbContext _context;

        public GetAllPendingOrdersQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async Task GetAllPendingOrders_WhenCalled_ShouldReturnOrderListViewModel()
        {
            var sut = new GetAllPendingOrdersQueryHandler(_context);

            var result = await sut.Handle(new GetAllPendingOrdersQuery(), CancellationToken.None);

            result.ShouldBeOfType<OrderListViewModel>();
        }

        [Fact]
        public async Task GetAllPendingOrders_WhenCalled_ShouldReturnCountOfPendingOrders()
        {
            var sut = new GetAllPendingOrdersQueryHandler(_context);
            var countOfPendingOrdersInDb = _context.Order.Where(o => !o.AcceptedByUserId.HasValue).Count();

            var result = await sut.Handle(new GetAllPendingOrdersQuery(), CancellationToken.None);
            var resultCount = result.Orders.Count();

            resultCount.ShouldBe(countOfPendingOrdersInDb);
        }
    }
}