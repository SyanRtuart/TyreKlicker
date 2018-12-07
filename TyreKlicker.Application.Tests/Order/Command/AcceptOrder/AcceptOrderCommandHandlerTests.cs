using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shouldly;
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
        private static readonly Guid UserId= Guid.Parse("2220d661-6a96-4537-a896-5014374d39f5");


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

            await sut.Handle(new AcceptOrderCommand { OrderId = order.Id, UserId = UserId }, CancellationToken.None);
            var orderInDb = await _context.Order.SingleOrDefaultAsync(o => o.Id == order.Id, CancellationToken.None);

            orderInDb.AcceptedByUserId.ShouldBe(UserId);

        }
    }
}
