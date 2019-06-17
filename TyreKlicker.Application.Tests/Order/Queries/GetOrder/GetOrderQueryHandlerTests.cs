

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Shouldly;
using TyreKlicker.Application.Exceptions;
using TyreKlicker.Application.Order.Queries.GetAllOrders;
using TyreKlicker.Application.Order.Queries.GetOrder;
using TyreKlicker.Application.Tests.Infrastructure;
using TyreKlicker.Persistence;
using Xunit;
using OrderDto = TyreKlicker.Application.Order.Models.OrderDto;

namespace TyreKlicker.Application.Tests.Order.Queries.GetOrder
{
    [Collection("QueryCollection")]
    public class GetOrderQueryHandlerTests
    {
        private readonly TyreKlickerDbContext _context;
        private readonly IMapper _mapper;

        public GetOrderQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetOrderQueryHandler_WhenCalled_ShouldReturnOrderListViewModel()
        {
            var sut = new GetOrderQueryHandler(_context, _mapper);
            var orderInDb = _context.Order.First();

            var result = await sut.Handle(new GetOrderQuery{Id = orderInDb.Id}, CancellationToken.None);

            result.ShouldBeOfType<OrderDto>();
        }

        [Fact]
        public async Task GetOrderQueryHandler_OrderDoesNotExist_ShouldThrowNotFoundException()
        {
            var sut = new GetOrderQueryHandler(_context, _mapper);

            await sut.Handle(new GetOrderQuery { Id = Guid.NewGuid() }, CancellationToken.None)
                
            .ShouldThrowAsync<NotFoundException>();
        }
    }
}
