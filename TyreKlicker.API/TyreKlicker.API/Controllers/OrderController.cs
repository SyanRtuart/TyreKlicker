using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TyreKlicker.Application.Order.Command.AcceptOrder;
using TyreKlicker.Application.Order.Command.CreateOrder;
using TyreKlicker.Application.Order.Queries.GetAllPendingOrders;

namespace TyreKlicker.API.Controllers
{
    //[Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class OrderController : BaseController
    {
        [HttpGet]
        public Task<OrderListViewModel> GetAll()
        {
            return Mediator.Send(new GetAllPendingOrdersQuery());
        }

        [HttpPost]
        public async Task<IActionResult> Order(CreateOrderCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [Route("{orderId:guid}/[action]")]
        [HttpPatch]
        public async Task<IActionResult> AcceptOrder(AcceptOrderCommand command, [FromRoute] Guid orderId)
        {
            if (command.OrderId != orderId) return BadRequest();
            
            return Ok(await Mediator.Send(command));
        }
    }
}