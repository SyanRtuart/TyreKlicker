using System.Threading.Tasks;

namespace TyreKlicker.XF.Core.Services.Payment
{
    public interface IPaymentService
    {
        Task<Models.Payment.Payment> MakePayment(Models.Payment.Payment payment, string secretKey);
    }
}