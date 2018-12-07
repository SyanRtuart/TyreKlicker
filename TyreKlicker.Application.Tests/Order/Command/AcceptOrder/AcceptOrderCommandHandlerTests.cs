using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using TyreKlicker.Application.Exceptions;
using TyreKlicker.Application.Order.Command.AcceptOrder;
using TyreKlicker.Application.Tests.Infrastructure;
using TyreKlicker.Persistence;
using Xunit;

namespace TyreKlicker.Application.Tests.Order.Command.AcceptOrder
{
    [Collection("QueryCollection")]
    public class AcceptOrderCommandHandlerTests
    {
        private readonly TyreKlickerDbContext _context;
        private static readonly Guid UserIdInDb = Guid.Parse("2220d661-6a96-4537-a896-5014374d39f5");


        public AcceptOrderCommandHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async Task AcceptOrderCommand_ValidParameters_ShouldSetAcceptedByUserIdToId()
        {
            var sut = new AcceptOrderCommandHandler(_context);
            var order = new Domain.Entities.Order()
            {
                Id = Guid.NewGuid(),
                AcceptedByUserId = Guid.Empty
            };
            _context.Order.Add(order);
            _context.SaveChanges();

            await sut.Handle(new AcceptOrderCommand { OrderId = order.Id, UserId = UserIdInDb }, CancellationToken.None);
            var orderInDb = await _context.Order.SingleOrDefaultAsync(o => o.Id == order.Id, CancellationToken.None);

            orderInDb.AcceptedByUserId.ShouldBe(UserIdInDb);
        }

        [Fact]
        public async Task AcceptOrderCommand_OrderDoesNotExist_ShouldThrowException()
        {
            var sut = new AcceptOrderCommandHandler(_context);
            var orderId = Guid.NewGuid();

            await sut.Handle(new AcceptOrderCommand { OrderId = orderId, UserId = UserIdInDb }, CancellationToken.None)
                
            .ShouldThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task AcceptOrderCommand_OrderIdIsEmpty_ShouldThrowException()
        {
            var sut = new AcceptOrderCommandHandler(_context);
            var order = new Domain.Entities.Order()
            {
                Id = Guid.NewGuid(),
                AcceptedByUserId = Guid.Empty
            };
            _context.Order.Add(order);
            _context.SaveChanges();

            await sut.Handle(new AcceptOrderCommand { OrderId = Guid.Empty , UserId = UserIdInDb }, CancellationToken.None)

            .ShouldThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task AcceptOrderCommand_UserDoesNotExist_ShouldThrowException()
        {
            var sut = new AcceptOrderCommandHandler(_context);
            var order = new Domain.Entities.Order()
            {
                Id = Guid.NewGuid(),
                AcceptedByUserId = Guid.Empty
            };
            _context.Order.Add(order);
            _context.SaveChanges();

            await sut.Handle(new AcceptOrderCommand { OrderId = order.Id, UserId = Guid.NewGuid() }, CancellationToken.None)

            .ShouldThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task AcceptOrderCommand_UserIdIsEmpty_ShouldThrowException()
        {
            var sut = new AcceptOrderCommandHandler(_context);
            var order = new Domain.Entities.Order()
            {
                Id = Guid.NewGuid(),
                AcceptedByUserId = Guid.Empty
            };
            _context.Order.Add(order);
            _context.SaveChanges();

            await sut.Handle(new AcceptOrderCommand { OrderId = order.Id, UserId = Guid.Empty}, CancellationToken.None)

            .ShouldThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task AcceptOrderCommand_AlreadyAccepted_ShouldThrowException()
        {
            var sut = new AcceptOrderCommandHandler(_context);
            var order = new Domain.Entities.Order()
            {
                Id = Guid.NewGuid(),
                AcceptedByUserId = Guid.NewGuid()
            };
            _context.Order.Add(order);
            _context.SaveChanges();

            await sut.Handle(new AcceptOrderCommand { OrderId = order.Id, UserId = UserIdInDb }, CancellationToken.None)

            .ShouldThrowAsync<AlreadyAcceptedException>();
        }
    }
}
