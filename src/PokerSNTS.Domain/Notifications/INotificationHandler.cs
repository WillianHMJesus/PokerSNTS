using FluentValidation.Results;
using System.Collections.Generic;

namespace PokerSNTS.Domain.Notifications
{
    public interface INotificationHandler
    {
        void HandleNotification(string key, string value);
        void HandleNotification(ValidationResult validationResult);
        bool HasNotification();
        List<Notification> GetNotifications();
    }
}
