using PokerSNTS.Domain.Entities;
using PokerSNTS.Domain.Interfaces.Repositories;
using PokerSNTS.Domain.Interfaces.Services;
using PokerSNTS.Domain.Interfaces.UnitOfWork;
using PokerSNTS.Domain.Notifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDomainNotificationHandler _notification;

        public PlayerService(IPlayerRepository playerRepository, 
            IUnitOfWork unitOfWork, 
            IDomainNotificationHandler notification)
        {
            _playerRepository = playerRepository;
            _unitOfWork = unitOfWork;
            _notification = notification;
        }

        public async Task<bool> Add(Player player)
        {
            if(player.IsValid)
            {
                _playerRepository.Add(player);

                return await _unitOfWork.Commit();
            }

            _notification.HandleNotification(player.ValidationResult);

            return false;
        }

        public async Task<bool> Update(Player player)
        {
            if(player.IsValid)
            {
                _playerRepository.Update(player);

                return await _unitOfWork.Commit();
            }

            _notification.HandleNotification(player.ValidationResult);

            return false;
        }

        public async Task<IEnumerable<Player>> GetAll()
        {
            return await _playerRepository.GetAll();
        }
    }
}
