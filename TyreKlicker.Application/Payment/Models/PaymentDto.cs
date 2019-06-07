namespace TyreKlicker.Application.Payment.Models
{
    public class PaymentDto
    {
        public string ExternalPaymentId { get; set; }

        public string Email { get; set; }

        public string Token { get; set; }

        public long Amount { get; set; }

        public string Description { get; set; }

        public string Currency { get; set; }
    }
}