﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TyreKlicker.Application.Job.Command.CreateJob;
using TyreKlicker.Application.Order.Queries.GetAllPendingOrders;

namespace TyreKlicker.API.Controllers
{
    //[Produces("application/json")]
    //[Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class OrderController : BaseController
    {
        [HttpGet]
        public Task<OrderListViewModel> Get()
        {
            return Mediator.Send(new GetAllPendingOrdersQuery());
        }

        //[HttpGet]
        //public Task<OrderListViewModel> Get(bool pending = true)
        //{
        //    return Mediator.Send(new GetAllPendingOrdersQuery());
        //}

        [HttpPost]
        public async Task<IActionResult> Order(CreateOrderCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}