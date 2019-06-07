namespace TyreKlicker.Domain.Entities
{
    public class Payment : Entity
    {
        public string ExternalPaymentId { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public string Description { get; set; }
    }
}