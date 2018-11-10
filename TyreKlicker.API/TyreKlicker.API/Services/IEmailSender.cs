using System.Threading.Tasks;

namespace TyreKlicker.API.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}