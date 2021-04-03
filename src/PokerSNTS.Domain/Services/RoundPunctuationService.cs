using PokerSNTS.Domain.Entities;
using PokerSNTS.Domain.Interfaces.Repositories;
using PokerSNTS.Domain.Interfaces.Services;
using PokerSNTS.Domain.Interfaces.UnitOfWork;
using PokerSNTS.Domain.Notifications;
using System;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Services
{
    public class RoundPunctuationService : BaseService, IRoundPunctuationService
    {
        private readonly IRoundPunctuationRepository _roundPunctuationRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IRoundRepository _roundRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDomainNotificationHandler _notifications;

        public RoundPunctuationService(IRoundPunctuationRepository roundPunctuationRepository,
            IPlayerRepository playerRepository,
            IRoundRepository roundRepository,
            IUnitOfWork unitOfWork,
            IDomainNotificationHandler notifications)
            : base(notifications)
        {
            _roundPunctuationRepository = roundPunctuationRepository;
            _playerRepository = playerRepository;
            _roundRepository = roundRepository;
            _unitOfWork = unitOfWork;
            _notifications = notifications;
        }

        public async Task<RoundPunctuation> AddAsync(RoundPunctuation roundPunctuation)
        {
            if (await ValidateRoundPunctuationAsync(roundPunctuation))
            {
                _roundPunctuationRepository.Add(roundPunctuation);
                if (await _unitOfWork.CommitAsync()) return roundPunctuation;
            }

            return null;
        }

        public async Task<RoundPunctuation> UpdateAsync(Guid id, RoundPunctuation roundPunctuation)
        {
            var existingRoundPunctuation = await _roundPunctuationRepository.GetByIdAsync(id);
            if (existingRoundPunctuation == null)
            {
                _notifications.HandleNotification("DomainValidation", "Partida do jogador não encontrada.");
                return null;
            }

            existingRoundPunctuation.Update(roundPunctuation.Position, roundPunctuation.Punctuation, roundPunctuation.PlayerId, roundPunctuation.RoundId);
            if (await ValidateRoundPunctuationAsync(existingRoundPunctuation))
            {
                _roundPunctuationRepository.Update(existingRoundPunctuation);
                if (await _unitOfWork.CommitAsync()) return existingRoundPunctuation;
            }

            return null;
        }

        private async Task<bool> ValidateRoundPunctuationAsync(RoundPunctuation roundPunctuation)
        {
            var validationEntity = ValidateEntity(roundPunctuation);
            var validationPlayer = await ValidatePlayerExistsAsync(roundPunctuation.PlayerId);
            var validationRound = await ValidateRoundExistsAsync(roundPunctuation.RoundId);

            return validationEntity && validationPlayer && validationRound;
        }

        private async Task<bool> ValidatePlayerExistsAsync(Guid playerId)
        {
            var player = await _playerRepository.GetByIdAsync(playerId);
            if (player != null) return true;

            _notifications.HandleNotification("DomainValidation", "Jogador não encontrado.");

            return false;
        }

        private async Task<bool> ValidateRoundExistsAsync(Guid roundId)
        {
            var round = await _roundRepository.GetByIdAsync(roundId);
            if (round != null) return true;

            _notifications.HandleNotification("DomainValidation", "Rodada não encontrada.");

            return false;
        }
    }
}
