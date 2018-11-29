using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TyreKlicker.Application.Order.Queries.GetAllOrdersCreatedByUser;

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
        public Task<OrderCreatedByUserListViewModel> OrdersAccepted(Guid userId)
        {
            return Mediator.Send(new GetAllOrdersCreatedByUserQuery() { UserId = userId });
        }
    }
}