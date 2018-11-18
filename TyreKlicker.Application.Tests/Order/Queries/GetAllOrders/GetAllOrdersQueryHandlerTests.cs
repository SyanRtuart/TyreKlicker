using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using TyreKlicker.Application.Order.Queries.GetAllOrders;
using TyreKlicker.Application.Tests.Infrastructure;
using TyreKlicker.Persistence;
using Xunit;

namespace TyreKlicker.Application.Tests.Order.Queries.GetAllOrders
{
    [Collection("QueryCollection")]
    public class GetAllOrdersQueryHandlerTests
    {
        private readonly TyreKlickerDbContext _context;

        public GetAllOrdersQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async Task GetAllOrders_WhenCalled_ShouldReturnOrderListViewModel()
        {
            var sut = new GetAllOrdersQueryHandler(_context);

            var result = await sut.Handle(new GetAllOrdersQuery(), CancellationToken.None);

            result.ShouldBeOfType<OrderListViewModel>();
        }
    }
}