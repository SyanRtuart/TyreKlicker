using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Shouldly;
using TyreKlicker.Application.Exceptions;
using TyreKlicker.Application.Order.Command.CreateOrder;
using TyreKlicker.Application.Tests.Infrastructure;
using TyreKlicker.Persistence;
using Xunit;

namespace TyreKlicker.Application.Tests.Order.Command.CreateOrder
{
    [Collection("CommandCollection")]
    public class CreateOrderCommandHandlerTests
    {
        public CreateOrderCommandHandlerTests(CommandTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        private readonly TyreKlickerDbContext _context;
        private readonly IMapper _mapper;


        [Fact]
        public async Task CreateOrderCommand_AddressDoesNotExist_ShouldThrowException()
        {
            var sut = new CreateOrderCommandHandler(_context, _mapper);
            var currentUser = _context.User.First();

            await sut.Handle(new CreateOrderCommand {CreatedByUserId = currentUser.Id, AddressId = Guid.NewGuid()},
                    CancellationToken.None)
                .ShouldThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task CreateOrderCommand_AfterModelHasBeenValidated_OrderCountShouldIncrementBy1()
        {
            var sut = new CreateOrderCommandHandler(_context, _mapper);
            var orderCountBeforeAct = _context.Order.ToList().Count;
            var currentUser = _context.User.First();
            var currentAddress = _context.Address.First(x => x.UserId == currentUser.Id);

            await sut.Handle(
                new CreateOrderCommand
                {
                    CreatedByUserId = currentUser.Id, AddressId = currentAddress.Id, Make = "Audi", Model = "A7",
                    Trim = "14", Registration = "X55 XXX", Tyre = "Tyre", Year = "2012", Description = "Desc"
                },
                CancellationToken.None);

            var orderCountAfterAct = _context.Order.ToList().Count;

            orderCountAfterAct.ShouldBe(orderCountBeforeAct + 1);
        }

        [Fact]
        public async Task CreateOrderCommand_UserDoesNotExist_ShouldThrowException()
        {
            var sut = new CreateOrderCommandHandler(_context, _mapper);

            await sut.Handle(new CreateOrderCommand {CreatedByUserId = Guid.NewGuid()}, CancellationToken.None)
                .ShouldThrowAsync<NotFoundException>();
        }
    }
}