using PokerSNTS.Domain.Entities;
using PokerSNTS.Domain.Notifications;

namespace PokerSNTS.Domain.Services
{
    public abstract class BaseService
    {
        private readonly IDomainNotificationHandler _notifications;

        public BaseService(IDomainNotificationHandler notifications)
        {
            _notifications = notifications;
        }

        protected bool ValidateEntity<T>(T entity) where T : Entity
        {
            var validationResult = entity.Validate();
            if (validationResult.IsValid) return true;

            _notifications.HandleNotification(validationResult);
            return false;
        }
    }
}
