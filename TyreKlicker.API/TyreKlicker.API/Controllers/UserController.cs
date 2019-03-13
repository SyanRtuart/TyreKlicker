using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TyreKlicker.Application.Order.Queries.GetAllOrdersAcceptedByUser;
using TyreKlicker.Application.Order.Queries.GetAllOrdersCreatedByUser;
using TyreKlicker.Application.Order.Queries.GetCurrentOrders;
using TyreKlicker.Application.User.Queries.GetUserByEmail;

namespace TyreKlicker.API.Controllers
{
    [ApiController]
    //[Route("[controller]/[action]")]
    public class UserController : BaseController
    {
        [HttpGet, Route("~/api/user/{userId:guid}/[action]")]
        public Task<OrderCreatedByUserListViewModel> OrdersCreated(Guid userId)
        {
            return Mediator.Send(new GetAllOrdersCreatedByUserQuery() { UserId = userId });
        }

        [HttpGet, Route("~/api/user/{userId:guid}/[action]")]
        public Task<OrderAcceptedByUserListViewModel> OrdersAccepted(Guid userId)
        {
            return Mediator.Send(new GetAllOrdersAcceptedByUserQuery() { UserId = userId });
        }

        [HttpGet, Route("~/api/user/[action]")]
        public Task<UserViewModel> Email(string email)
        {
            return Mediator.Send(new GetUserByEmailQuery() { Email = email });
        }

        [HttpGet, Route("~/api/{userId:guid}/[action]")]
        public async Task<CurrentOrdersListViewModel> Orders([FromRoute] Guid userId)
        {
            return await Mediator.Send(new GetCurrentOrdersQuery() { UserId = userId });
        }
    }
}