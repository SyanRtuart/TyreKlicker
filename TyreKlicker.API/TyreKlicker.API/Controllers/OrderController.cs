using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using TyreKlicker.Application.Job.Command.CreateJob;
using TyreKlicker.Application.Order.Queries;

namespace TyreKlicker.API.Controllers
{
    //[Produces("application/json")]
    //[Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class OrderController : BaseController
    {
        [HttpGet]
        public Task<OrderListViewModel> GetAll()
        {
            //var asda = LoggedInUser;
            return Mediator.Send(new GetAllOrdersQuery());
        }

        [HttpPost]
        public async Task<IActionResult> Order([FromQuery]CreateOrderCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}