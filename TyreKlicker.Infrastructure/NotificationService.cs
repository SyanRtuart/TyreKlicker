using System.Threading.Tasks;
using TyreKlicker.Application.Interfaces;
using TyreKlicker.Application.Notifications.Models;

namespace TyreKlicker.Infrastructure
{
    public class NotificationService : INotificationService
    {
        public Task SendAsync(Message message)
        {
            return Task.CompletedTask;
        }
    }
}