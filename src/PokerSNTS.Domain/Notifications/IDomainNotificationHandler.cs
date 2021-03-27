using FluentValidation.Results;
using System.Collections.Generic;

namespace PokerSNTS.Domain.Notifications
{
    public interface IDomainNotificationHandler
    {
        void HandleNotification(ValidationResult validationResult);
        bool HasNotification();
        List<DomainNotification> GetNotifications();
    }
}
