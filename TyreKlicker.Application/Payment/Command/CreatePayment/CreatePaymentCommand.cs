using MediatR;

namespace TyreKlicker.Application.Payment.Command.CreatePayment
{
    public class CreatePaymentCommand : IRequest
    {
        public string Email { get; set; }

        public string Token { get; set; }

        public long Amount { get; set; }

        public string Description { get; set; }

        public string Currency { get; set; }
    }
}