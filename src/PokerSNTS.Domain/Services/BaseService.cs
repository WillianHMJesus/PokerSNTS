using PokerSNTS.Domain.Entities;
using PokerSNTS.Domain.Interfaces.UnitOfWork;
using PokerSNTS.Domain.Notifications;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Services
{
    public abstract class BaseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationHandler _notifications;

        public BaseService(IUnitOfWork unitOfWork,
            INotificationHandler notifications)
        {
            _unitOfWork = unitOfWork;
            _notifications = notifications;
        }

        protected async Task<bool> CommitAsync()
        {
            return await _unitOfWork.CommitAsync();
        }

        protected bool ValidateEntity<T>(T entity) where T : Entity
        {
            var validationResult = entity.Validate();
            if (validationResult.IsValid) return true;

            _notifications.HandleNotification(validationResult);
            return false;
        }

        protected void AddNotification(string message)
        {
            _notifications.HandleNotification("DomainValidation", message);
        }
    }
}
