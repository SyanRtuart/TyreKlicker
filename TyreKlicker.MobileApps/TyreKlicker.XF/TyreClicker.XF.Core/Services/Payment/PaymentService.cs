using System.Threading.Tasks;
using TyreKlicker.XF.Core.Services.RequestProvider;

namespace TyreKlicker.XF.Core.Services.Payment
{
    internal class PaymentService : IPaymentService
    {
        private readonly IRequestProvider _requestProvider;

        public PaymentService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<Models.Payment.Payment> MakePayment(Models.Payment.Payment payment, string secretKey)
        {
            var uri = GlobalSetting.Instance.PaymentEndPoint;

            var result = await _requestProvider.PostAsync(uri, payment, GlobalSetting.Instance.AuthToken);

            return result;
        }
    }
}