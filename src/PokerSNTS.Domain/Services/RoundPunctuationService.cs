using PokerSNTS.Domain.Entities;
using PokerSNTS.Domain.Interfaces.Repositories;
using PokerSNTS.Domain.Interfaces.Services;
using PokerSNTS.Domain.Interfaces.UnitOfWork;
using PokerSNTS.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Services
{
    public class RoundPunctuationService : BaseService, IRoundPunctuationService
    {
        private readonly IRoundPunctuationRepository _roundPunctuationRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IRoundRepository _roundRepository;

        public RoundPunctuationService(IRoundPunctuationRepository roundPunctuationRepository,
            IPlayerRepository playerRepository,
            IRoundRepository roundRepository,
            IUnitOfWork unitOfWork,
            INotificationHandler notifications)
            : base(unitOfWork, notifications)
        {
            _roundPunctuationRepository = roundPunctuationRepository;
            _playerRepository = playerRepository;
            _roundRepository = roundRepository;
        }

        public async Task AddAsync(RoundPunctuation roundPunctuation)
        {
            var roundsPunctuations = await GetAllAsync();

            if (roundsPunctuations.Any(x => x.PlayerId == roundPunctuation.PlayerId && x.RoundId == roundPunctuation.RoundId))
                AddNotification("Esse jogador já tem posição para essa rodada.");

            if (await ValidateRoundPunctuationAsync(roundPunctuation))
            {
                _roundPunctuationRepository.Add(roundPunctuation);

                if (!await CommitAsync())
                    AddNotification("Não foi possível cadastrar a pontuação da rodada.");
            }
        }

        public async Task UpdateAsync(Guid id, RoundPunctuation roundPunctuation)
        {
            var existingRoundPunctuation = await _roundPunctuationRepository.GetByIdAsync(id);

            if (existingRoundPunctuation == null) 
                AddNotification("Partida do jogador não encontrada.");

            existingRoundPunctuation.Update(
                roundPunctuation.Position, roundPunctuation.Punctuation, roundPunctuation.PlayerId, roundPunctuation.RoundId);
            if (await ValidateRoundPunctuationAsync(existingRoundPunctuation))
            {
                _roundPunctuationRepository.Update(existingRoundPunctuation);

                if (!await CommitAsync()) 
                    AddNotification("Não foi possível atualizar a pontuação da rodada.");
            }
        }

        public async Task<IEnumerable<RoundPunctuation>> GetAllAsync()
        {
            return await _roundPunctuationRepository.GetAllAsync();
        }

        public async Task<RoundPunctuation> GetByIdAsync(Guid id)
        {
            return await _roundPunctuationRepository.GetByIdAsync(id);
        }

        #region Validate
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

            AddNotification("Jogador não encontrado.");

            return false;
        }

        private async Task<bool> ValidateRoundExistsAsync(Guid roundId)
        {
            var round = await _roundRepository.GetByIdAsync(roundId);
            if (round != null) return true;

            AddNotification("Rodada não encontrada.");

            return false;
        }
        #endregion
    }
}
