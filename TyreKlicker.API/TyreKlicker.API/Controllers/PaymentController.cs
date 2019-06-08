using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TyreKlicker.Application.Payment.Command.CreatePayment;

namespace TyreKlicker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Post(CreatePaymentCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}