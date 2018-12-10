﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using TyreKlicker.Application.Order.Command.CompleteOrder;
using TyreKlicker.Application.Tests.Infrastructure;
using TyreKlicker.Application.Tests.Order.Command.AcceptOrder;
using TyreKlicker.Persistence;
using Xunit;

namespace TyreKlicker.Application.Tests.Order.Command.CompleteOrder
{
    [Collection("QueryCollection")]
    public class CompleteOrderCommandHandlerTests : IClassFixture<CompleteOrderTestFixtures>
    {
        private readonly CompleteOrderTestFixtures _fixtures;

        public CompleteOrderCommandHandlerTests(CompleteOrderTestFixtures fixtures)
        {
            _fixtures = fixtures;
        }

        [Fact]
        public async Task CompleteOrderCommand_OrderIsNotComplete_ShouldSetCompleteToTrue()
        {
            var sut = new CompleteOrderCommandHandler(_fixtures.Context);

            await sut.Handle(new CompleteOrderCommand {OrderId = _fixtures.Order.Id, Complete = true}, CancellationToken.None);
            var orderInDb = await _fixtures.Context.Order.SingleOrDefaultAsync(o => o.Id == _fixtures.Order.Id, CancellationToken.None);

            orderInDb.Complete.ShouldBe(true);
        }
    }

    [Collection("QueryCollection")]
    public class CompleteOrderTestFixtures : IDisposable
    {
        public TyreKlickerDbContext Context { get; set; }
        public Domain.Entities.Order Order;

        public CompleteOrderTestFixtures(QueryTestFixture fixture)
        {
            Context = fixture.Context;

            Order = new Domain.Entities.Order()
            {
                Id = Guid.NewGuid(),
                AcceptedByUserId = Guid.NewGuid(),
                Complete = false
            };
            Context.Order.Add(Order);
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Order.RemoveRange(Order);
        }

    }
}