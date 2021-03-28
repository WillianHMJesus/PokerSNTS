using FluentValidation.Results;
using System.Collections.Generic;

namespace PokerSNTS.Domain.Notifications
{
    public interface IDomainNotificationHandler
    {
        void HandleNotification(string key, string value);
        void HandleNotification(ValidationResult validationResult);
        bool HasNotification();
        List<DomainNotification> GetNotifications();
    }
}
