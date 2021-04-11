using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace PokerSNTS.Domain.Notifications
{
    public class NotificationHandler : INotificationHandler
    {
        private List<Notification> _notifications;

        public NotificationHandler()
        {
            _notifications = new List<Notification>();
        }

        public void HandleNotification(ValidationResult validationResult)
        {
            foreach (var error in validationResult?.Errors ?? Enumerable.Empty<ValidationFailure>())
            {
                _notifications.Add(new Notification("DomainValidation", error.ErrorMessage));
            }
        }

        public void HandleNotification(string key, string value)
        {
            _notifications.Add(new Notification(key, value));
        }

        public bool HasNotification()
        {
            return _notifications.Any();
        }

        public List<Notification> GetNotifications()
        {
            return _notifications;
        }

        public void Dispose()
        {
            _notifications = new List<Notification>();
        }
    }
}
