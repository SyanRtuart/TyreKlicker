using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TyreKlicker.Application.Job.Command.CreateJob;
using TyreKlicker.Application.Order.Queries.GetAllOrders;

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
            return Mediator.Send(new GetAllOrdersQuery());
        }

        [HttpPost]
        public async Task<IActionResult> Order(CreateOrderCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}