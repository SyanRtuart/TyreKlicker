using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shouldly;
using TyreKlicker.Application.Order.Queries.GetAllOrdersAcceptedByUser;
using TyreKlicker.Application.Tests.Infrastructure;
using TyreKlicker.Persistence;
using Xunit;
using Xunit.Extensions;

namespace TyreKlicker.Application.Tests.Order.Queries.GetAllOrdersAcceptedByUser
{
    [Collection("QueryCollection")]
    public class GetAllOrdersAcceptedByUserQueryHandlerTests
    {
        private readonly TyreKlickerDbContext _context;
        private static readonly Guid UserIdToTest = Guid.Parse("1114e3c0-093f-4e18-be42-d7c3e178a22c");


        public GetAllOrdersAcceptedByUserQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            
        }


        [Fact]
        public async Task GetAllOrdersAcceptedByUser_WhenCalled_ShouldReturnOrderListViewModel()
        {
            var sut = new GetAllOrdersAcceptedByUserQueryHandler(_context);

            var result = await sut.Handle(new GetAllOrdersAcceptedByUserQuery(), CancellationToken.None);

            result.ShouldBeOfType<OrderAcceptedByUserListViewModel>();
        }

        [Theory, MemberData(nameof(TestGuids))]
        public async Task GetAllOrdersAcceptedByUser_WhenCalled_ShouldReturnCountOfOrdersAcceptedByGivenUser(Guid userGuid2, Guid userGuid3)
        {
            var sut = new GetAllOrdersAcceptedByUserQueryHandler(_context);
            var orders = new List<Domain.Entities.Order>()
            {
                new Domain.Entities.Order { AcceptedByUserId = UserIdToTest},
                new Domain.Entities.Order { AcceptedByUserId = userGuid2},
                new Domain.Entities.Order { AcceptedByUserId = userGuid3}
            };
            _context.Order.AddRange(orders);
            _context.SaveChanges();
            var acceptedOrdersInDb =  _context.Order.Count(x => x.AcceptedByUserId == UserIdToTest);

            var result = await sut.Handle(new GetAllOrdersAcceptedByUserQuery() { UserId = UserIdToTest }, CancellationToken.None);
            var resultCount = result.Orders.Count();


            resultCount.ShouldBe(acceptedOrdersInDb);
        }

        public static IEnumerable<object[]> TestGuids
        {           
            get
            {               
                yield return new object[] {  Guid.NewGuid() , Guid.NewGuid() };
                yield return new object[] {  UserIdToTest , Guid.NewGuid() };
                yield return new object[] {  UserIdToTest, UserIdToTest };
            }
        }

  
    }
}
