using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TyreKlicker.Application.Interfaces;
using TyreKlicker.Application.Payment.Command.CreatePayment;
using TyreKlicker.Application.Payment.Models;
using TyreKlicker.Infrastructure.Models;

namespace TyreKlicker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;
        private readonly StripeSettings _stripeSettings;

        public PaymentController(IOptions<StripeSettings> stripeSettings, IPaymentService paymentService)
        {
            _paymentService = paymentService;
            _stripeSettings = stripeSettings.Value;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreatePaymentCommand command)
        {
            var paymentId = await _paymentService.MakePayment(new PaymentDto
            {
                Description = command.Description,
                Currency = command.Currency,
                Email = command.Email,
                Amount = command.Amount,
                Token = command.Token
            }, _stripeSettings.SecretKet);

            command.ExternalPaymentId = paymentId;

            return Ok(await Mediator.Send(command));
        }


        //[HttpPost]
        //public IActionResult MakePayment([FromForm] string stripeEmail, [FromForm] string stripeToken)
        //{
        //    var customerService = new CustomerService();
        //    var chargeService = new ChargeService();

        //    var customer = customerService.Create(new CustomerCreateOptions
        //    {
        //        Email = stripeEmail,
        //        SourceToken = stripeToken
        //    });

        //    var charge = chargeService.Create(new ChargeCreateOptions
        //    {
        //        Amount = 200000000,
        //        Description = "From Ainsley",
        //        Currency = "GBP",
        //        CustomerId = customer.Id
        //    });

        //    return null;
        //}
    }
}