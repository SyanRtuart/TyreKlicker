using TyreKlicker.Application.Notifications.Models;
using System.Threading.Tasks;
using TyreKlicker.Application.Interfaces;

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