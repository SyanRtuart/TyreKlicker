using System;
using System.Collections.Generic;
using System.Text;

namespace TyreKlicker.XF.Core.Services.Stripe
{
    public interface IStripeService
    {
        string CreateToken(string cardNumber, long cardExpMonth, long cardExpYear, string cardCVC);
    }
}
