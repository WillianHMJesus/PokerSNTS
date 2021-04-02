using PokerSNTS.Domain.Entities;
using PokerSNTS.Domain.Interfaces.Repositories;
using PokerSNTS.Domain.Interfaces.Services;
using PokerSNTS.Domain.Interfaces.UnitOfWork;
using PokerSNTS.Domain.Notifications;
using System;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Services
{
    public class RoundPunctuationService : IRoundPunctuationService
    {
        private readonly IRoundPunctuationRepository _roundPunctuationRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IRoundRepository _roundRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDomainNotificationHandler _notification;

        public RoundPunctuationService(IRoundPunctuationRepository roundPunctuationRepository,
            IPlayerRepository playerRepository,
            IRoundRepository roundRepository,
            IUnitOfWork unitOfWork,
            IDomainNotificationHandler notification)
        {
            _roundPunctuationRepository = roundPunctuationRepository;
            _playerRepository = playerRepository;
            _roundRepository = roundRepository;
            _unitOfWork = unitOfWork;
            _notification = notification;
        }

        public async Task<bool> Add(RoundPunctuation roundPunctuation)
        {
            var validationResult = roundPunctuation.Validate();
            if(validationResult.IsValid)
            {
                if(await ValidateRelationships (roundPunctuation))
                {
                    _roundPunctuationRepository.Add(roundPunctuation);

                    return await _unitOfWork.Commit();
                }
            }

            _notification.HandleNotification(validationResult);

            return false;
        }

        public async Task<bool> Update(Guid id, RoundPunctuation roundPunctuation)
        {
            var existingRoundPunctuation = await _roundPunctuationRepository.GetById(id);
            if (existingRoundPunctuation == null) _notification.HandleNotification("DomainValidation", "Partida do jogador não encontrada.");

            if(!_notification.HasNotification())
            {
                existingRoundPunctuation.Update(roundPunctuation.Position, roundPunctuation.Punctuation, roundPunctuation.PlayerId, roundPunctuation.RoundId);
                var validationResult = existingRoundPunctuation.Validate();
                if (validationResult.IsValid)
                {
                    if (await ValidateRelationships(roundPunctuation))
                    {
                        _roundPunctuationRepository.Update(existingRoundPunctuation);

                        return await _unitOfWork.Commit();
                    }
                }

                _notification.HandleNotification(validationResult);
            }

            return false;
        }

        private async Task<bool> ValidateRelationships(RoundPunctuation roundPunctuation)
        {
            var validationPlayer = await ValidatePlayerExists(roundPunctuation.PlayerId);
            var validationRound = await ValidateRoundExists(roundPunctuation.RoundId);

            return validationPlayer && validationRound;
        }

        private async Task<bool> ValidatePlayerExists(Guid playerId)
        {
            var player = await _playerRepository.GetById(playerId);
            if(player == null)
            {
                _notification.HandleNotification("DomainValidation", "Jogador não encontrado.");

                return false;
            }

            return true;
        }

        private async Task<bool> ValidateRoundExists(Guid roundId)
        {
            var round = await _roundRepository.GetById(roundId);
            if(round == null)
            {
                _notification.HandleNotification("DomainValidation", "Rodada não encontrada.");

                return false;
            }

            return true;
        }
    }
}
