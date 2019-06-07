using System.Threading.Tasks;
using TyreKlicker.Application.Payment.Models;

namespace TyreKlicker.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<string> MakePayment(PaymentDto payment, string secretKey);
    }
}