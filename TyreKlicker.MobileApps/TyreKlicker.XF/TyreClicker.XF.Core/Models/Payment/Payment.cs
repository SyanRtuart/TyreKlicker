namespace TyreKlicker.XF.Core.Models.Payment
{
    public class Payment
    {
        public string Email { get; set; }

        public string Token { get; set; }

        public long Amount { get; set; }

        public string Description { get; set; }

        public string Currency { get; set; }
    }
}