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
        [Route("~/api/users/{userId:guid}/[action]")]
        [HttpGet]
        public Task<OrderCreatedByUserListViewModel> OrdersCreated(Guid userId)
        {
            return Mediator.Send(new GetAllOrdersCreatedByUserQuery() { UserId = userId });
        }

        [Route("~/api/users/{userId:guid}/[action]")]
        [HttpGet]
        public Task<OrderAcceptedByUserListViewModel> OrdersAccepted(Guid userId)
        {
            return Mediator.Send(new GetAllOrdersAcceptedByUserQuery() { UserId = userId });
        }

        [Route("~/api/users/[action]")]
        [HttpGet]
        public Task<UserViewModel> Email(string email)
        {
            return Mediator.Send(new GetUserByEmailQuery() { Email = email });
        }

        [Route("~/api/{userId:guid}/[action]")]
        [HttpGet]
        public async Task<CurrentOrdersListViewModel> Orders([FromRoute] Guid userId)
        {
            return await Mediator.Send(new GetCurrentOrdersQuery() { UserId = userId });
        }
    }
}