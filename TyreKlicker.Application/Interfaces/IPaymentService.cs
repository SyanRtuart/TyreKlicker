using System.Threading.Tasks;
using TyreKlicker.Application.Payment.Models;

namespace TyreKlicker.Application.Interfaces
{
    public interface IPaymentService
    {
        string SecretKey { get; set; }
        Task<string> MakePayment(PaymentDto payment, string secretKey);
    }
}