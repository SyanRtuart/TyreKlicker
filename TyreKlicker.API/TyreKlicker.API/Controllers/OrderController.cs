using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TyreKlicker.Application.Order.Command.AcceptOrder;
using TyreKlicker.Application.Order.Command.CreateOrder;
using TyreKlicker.Application.Order.Queries.GetAllPendingOrders;
using TyreKlicker.Application.Order.Queries.GetOrder;

namespace TyreKlicker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class OrderController : BaseController
    {
        [HttpGet]
        public async Task<OrderListViewModel> GetAll()
        {
            return await Mediator.Send(new GetAllPendingOrdersQuery());
        }

        [HttpGet, Route("{id:guid}")]
        public async Task<Application.Order.Models.OrderDto> GetOrder([FromRoute] Guid id)
        {
            return await Mediator.Send(new GetOrderQuery() { Id = id });
        }

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