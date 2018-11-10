using System.Threading.Tasks;
using TyreKlicker.Application.Notifications.Models;

namespace TyreKlicker.Application.Interfaces
{
    public interface INotificationService
    {
        Task SendAsync(Message message);
    }
}