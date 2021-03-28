using PokerSNTS.Domain.Entities;
using PokerSNTS.Domain.Interfaces.Repositories;
using PokerSNTS.Domain.Interfaces.Services;
using PokerSNTS.Domain.Interfaces.UnitOfWork;
using PokerSNTS.Domain.Notifications;
using System;
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
            var validationResult = playerRound.Validate();
            if(validationResult.IsValid)
            {
                _playerRoundRepository.Add(playerRound);

                return await _unitOfWork.Commit();
            }

            _notification.HandleNotification(validationResult);

            return false;
        }

        public async Task<bool> Update(Guid id, PlayerRound playerRound)
        {
            var existingPlayerRound = await _playerRoundRepository.GetById(id);
            if (existingPlayerRound == null) _notification.HandleNotification("DomainValidation", "Partida do jogador não encontrada.");

            if(!_notification.HasNotification())
            {
                existingPlayerRound.Update(playerRound.Position, playerRound.Punctuation, playerRound.PlayerId, playerRound.RoundId);
                var validationResult = existingPlayerRound.Validate();
                if (validationResult.IsValid)
                {
                    _playerRoundRepository.Update(existingPlayerRound);

                    return await _unitOfWork.Commit();
                }

                _notification.HandleNotification(validationResult);
            }

            return false;
        }
    }
}
