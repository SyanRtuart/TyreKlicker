using Shouldly;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TyreKlicker.Application.Exceptions;
using TyreKlicker.Application.Order.Command.CreateOrder;
using TyreKlicker.Application.Tests.Infrastructure;
using TyreKlicker.Persistence;
using Xunit;

namespace TyreKlicker.Application.Tests.Order.Command.CreateOrder
{
    [Collection("CreateOrderCommandHandlerCollection")]
    public class CreateOrderCommandHandlerTests
    {
        private readonly CreateOrderCommandHandlerFixture _fixture;

        public CreateOrderCommandHandlerTests(CreateOrderCommandHandlerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task CreateOrderCommand_AfterModelHasBeenValidated_OrderCountShouldIncrementBy1()
        {
            var sut = new CreateOrderCommandHandler(_fixture.Context);
            var orderCountBeforeAct = _fixture.Context.Order.ToList().Count;

            await sut.Handle(new CreateOrderCommand { CreatedByUserId = _fixture.CurrentUser.Id }, CancellationToken.None);

            var orderCountAfterAct = _fixture.Context.Order.ToList().Count;

            orderCountAfterAct.ShouldBe(orderCountBeforeAct + 1);
        }

        [Fact]
        public async Task CreateOrderCommand_UserDoesNotExist_ShouldThrowException()
        {
            var sut = new CreateOrderCommandHandler(_fixture.Context);

            await sut.Handle(new CreateOrderCommand { CreatedByUserId = Guid.NewGuid() }, CancellationToken.None)

            .ShouldThrowAsync<NotFoundException>();
        }
    }

    [Collection("CreateOrderCommandHandlerCollection")]
    public class CreateOrderCommandHandlerFixture : IDisposable
    {
        public TyreKlickerDbContext Context { get; set; }
        public Domain.Entities.User CurrentUser { get; set; }

        public CreateOrderCommandHandlerFixture()
        {
            Context = TyreKlickerContextFactory.Create();
            CurrentUser = new Domain.Entities.User();
            Context.Add(CurrentUser);
            Context.SaveChanges();
        }

        public void Dispose()
        {
            TyreKlickerContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("CreateOrderCommandHandlerCollection")]
    public class SetPrimaryAddressCommandHandlerCollection : ICollectionFixture<CreateOrderCommandHandlerFixture> { }
}