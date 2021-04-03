using PokerSNTS.Domain.Entities;
using PokerSNTS.Domain.Interfaces.Repositories;
using PokerSNTS.Domain.Interfaces.Services;
using PokerSNTS.Domain.Interfaces.UnitOfWork;
using PokerSNTS.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Services
{
    public class PlayerService : BaseService, IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDomainNotificationHandler _notifications;

        public PlayerService(IPlayerRepository playerRepository,
            IUnitOfWork unitOfWork,
            IDomainNotificationHandler notifications)
            : base(notifications)
        {
            _playerRepository = playerRepository;
            _unitOfWork = unitOfWork;
            _notifications = notifications;
        }

        public async Task<Player> AddAsync(Player player)
        {
            if (ValidateEntity(player))
            {
                _playerRepository.Add(player);
                if (await _unitOfWork.CommitAsync()) return player;
            }

            return null;
        }

        public async Task<Player> UpdateAsync(Guid id, Player player)
        {
            var existingPlayer = await _playerRepository.GetByIdAsync(id);
            if (existingPlayer == null)
            {
                _notifications.HandleNotification("DomainValidation", "Jogador não encontrado.");
                return null;
            }

            existingPlayer.Update(player.Name);
            if (ValidateEntity(existingPlayer))
            {
                _playerRepository.Update(existingPlayer);
                if (await _unitOfWork.CommitAsync()) return existingPlayer;
            }

            return null;
        }

        public async Task<IEnumerable<Player>> GetAllAsync()
        {
            return await _playerRepository.GetAllAsync();
        }
    }
}
