using Stripe;

namespace TyreKlicker.XF.Core.Services.Stripe
{
    public class StripeService : IStripeService
    {
        public string CreateToken(string cardNumber, long cardExpMonth, long cardExpYear, string cardCVC)
        {
            StripeConfiguration.SetApiKey(GlobalSetting.StripePublishableKey);

            var tokenOptions = new TokenCreateOptions
            {
                Card = new CreditCardOptions
                {
                    Number = cardNumber,
                    ExpMonth = cardExpMonth,
                    ExpYear = cardExpYear,
                    Cvc = cardCVC
                }
            };

            var tokenService = new TokenService();
            var stripeToken = tokenService.Create(tokenOptions);

            return stripeToken.Id;
        }
    }
}