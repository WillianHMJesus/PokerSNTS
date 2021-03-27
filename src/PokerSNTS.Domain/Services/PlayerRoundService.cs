using PokerSNTS.Domain.Entities;
using PokerSNTS.Domain.Interfaces.Repositories;
using PokerSNTS.Domain.Interfaces.Services;
using PokerSNTS.Domain.Interfaces.UnitOfWork;
using PokerSNTS.Domain.Notifications;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Services
{
    public class PlayerRoundService : IPlayerRoundService
    {
        private readonly IPlayerRoundRepository _playerRoundRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDomainNotificationHandler _notification;

        public PlayerRoundService(IPlayerRoundRepository playerRoundRepository,
            IUnitOfWork unitOfWork,
            IDomainNotificationHandler notification)
        {
            _playerRoundRepository = playerRoundRepository;
            _unitOfWork = unitOfWork;
            _notification = notification;
        }

        public async Task<bool> Add(PlayerRound playerRound)
        {
            if(playerRound.IsValid)
            {
                _playerRoundRepository.Add(playerRound);

                return await _unitOfWork.Commit();
            }

            _notification.HandleNotification(playerRound.ValidationResult);

            return false;
        }

        public async Task<bool> Update(PlayerRound playerRound)
        {
            if (playerRound.IsValid)
            {
                _playerRoundRepository.Update(playerRound);

                return await _unitOfWork.Commit();
            }

            _notification.HandleNotification(playerRound.ValidationResult);

            return false;
        }
    }
}
