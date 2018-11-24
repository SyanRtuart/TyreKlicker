using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TyreKlicker.Application.Interfaces;
using TyreKlicker.Application.Order.Command.CreateOrder;
using TyreKlicker.Application.Tests.Infrastructure;
using TyreKlicker.Persistence;
using Xunit;

namespace TyreKlicker.Application.Tests.Order.Command.CreateOrder
{
    [Collection("QueryCollection")]
    public class CreateOrderCommandHandlerTests
    {
        private readonly TyreKlickerDbContext _context;
        private readonly INotificationService _notificationService;

        public CreateOrderCommandHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async Task CreateOrderCommand_AfterModelHasBeenValidated_OrderCountShouldIncrementBy1()
        {
            var sut = new CreateOrderCommandHandler(_context, _notificationService);
            var orderCountBeforeAct = _context.Order.ToList().Count;

            await sut.Handle(new CreateOrderCommand { }, CancellationToken.None);

            var orderCountAfterAct = _context.Order.ToList().Count;

            orderCountAfterAct.ShouldBe(orderCountBeforeAct + 1);
        }
    }
}