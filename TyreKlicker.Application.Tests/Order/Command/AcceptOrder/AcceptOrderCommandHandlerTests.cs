using Microsoft.EntityFrameworkCore;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using TyreKlicker.Application.Exceptions;
using TyreKlicker.Application.Order.Command.AcceptOrder;
using TyreKlicker.Application.Tests.Infrastructure;
using TyreKlicker.Persistence;
using Xunit;

namespace TyreKlicker.Application.Tests.Order.Command.AcceptOrder
{
    [Collection("QueryCollection")]
    public class AcceptOrderCommandHandlerTests : IClassFixture<AcceptOrderTestsFixture>
    {
        private static readonly Guid UserIdInDb = Guid.Parse("2220d661-6a96-4537-a896-5014374d39f5");
        private readonly AcceptOrderTestsFixture _fixtures;

        public AcceptOrderCommandHandlerTests(AcceptOrderTestsFixture fixtures)
        {
            _fixtures = fixtures;
        }

        [Fact]
        public async Task AcceptOrderCommand_ValidParameters_ShouldSetAcceptedByUserIdToId()
        {
            var sut = new AcceptOrderCommandHandler(_fixtures.Context);

            await sut.Handle(new AcceptOrderCommand { OrderId = _fixtures.PendingOrder.Id, UserId = UserIdInDb }, CancellationToken.None);
            var orderInDb = await _fixtures.Context.Order.SingleOrDefaultAsync(o => o.Id == _fixtures.PendingOrder.Id, CancellationToken.None);

            orderInDb.AcceptedByUserId.ShouldBe(UserIdInDb);
        }

        [Fact]
        public async Task AcceptOrderCommand_OrderDoesNotExist_ShouldThrowException()
        {
            var sut = new AcceptOrderCommandHandler(_fixtures.Context);
            var orderId = Guid.NewGuid();

            await sut.Handle(new AcceptOrderCommand { OrderId = orderId, UserId = UserIdInDb }, CancellationToken.None)

            .ShouldThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task AcceptOrderCommand_OrderIdIsEmpty_ShouldThrowException()
        {
            var sut = new AcceptOrderCommandHandler(_fixtures.Context);

            await sut.Handle(new AcceptOrderCommand { OrderId = Guid.Empty, UserId = UserIdInDb }, CancellationToken.None)

            .ShouldThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task AcceptOrderCommand_UserDoesNotExist_ShouldThrowException()
        {
            var sut = new AcceptOrderCommandHandler(_fixtures.Context);
            _fixtures.AcceptedOrder.AcceptedByUserId = Guid.Empty;

            await sut.Handle(new AcceptOrderCommand { OrderId = _fixtures.AcceptedOrder.Id, UserId = Guid.NewGuid() }, CancellationToken.None)

            .ShouldThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task AcceptOrderCommand_UserIdIsEmpty_ShouldThrowException()
        {
            var sut = new AcceptOrderCommandHandler(_fixtures.Context);
            _fixtures.AcceptedOrder.AcceptedByUserId = Guid.Empty;

            await sut.Handle(new AcceptOrderCommand { OrderId = _fixtures.AcceptedOrder.Id, UserId = Guid.Empty }, CancellationToken.None)

            .ShouldThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task AcceptOrderCommand_AlreadyAccepted_ShouldThrowException()
        {
            var sut = new AcceptOrderCommandHandler(_fixtures.Context);

            await sut.Handle(new AcceptOrderCommand { OrderId = _fixtures.AcceptedOrder.Id, UserId = UserIdInDb }, CancellationToken.None)

            .ShouldThrowAsync<AlreadyAcceptedException>();
        }
    }

    [Collection("QueryCollection")]
    public class AcceptOrderTestsFixture
    {
        public TyreKlickerDbContext Context { get; set; }
        public Domain.Entities.Order AcceptedOrder;
        public Domain.Entities.Order PendingOrder;

        public AcceptOrderTestsFixture(QueryTestFixture fixture)
        {
            Context = fixture.Context;

            AcceptedOrder = new Domain.Entities.Order()
            {
                Id = Guid.NewGuid(),
                AcceptedByUserId = Guid.NewGuid()
            };
            PendingOrder = new Domain.Entities.Order()
            {
                Id = Guid.NewGuid(),
            };

            Context.Order.Add(AcceptedOrder);
            Context.Order.Add(PendingOrder);
            Context.SaveChanges();
        }
    }
}