using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TyreKlicker.Application.Order.Command.AcceptOrder;
using TyreKlicker.Application.Order.Command.CreateOrder;

namespace TyreKlicker.API.Controllers
{
    //[Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class OrderController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Order(CreateOrderCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost, Route("{orderId:guid}/[action]")]
        public async Task<IActionResult> AcceptOrder(AcceptOrderCommand command, [FromRoute] Guid orderId)
        {
            if (command.OrderId != orderId) return BadRequest();

            return Ok(await Mediator.Send(command));
        }

        [HttpPost, Route("{orderId:guid}/[action]")]
        public async Task<IActionResult> CompleteOrder(AcceptOrderCommand command, [FromRoute] Guid orderId)
        {
            if (command.OrderId != orderId) return BadRequest();

            return Ok(await Mediator.Send(command));
        }
    }
}