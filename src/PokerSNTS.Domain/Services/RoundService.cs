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
    public class RoundService : BaseService, IRoundService
    {
        private readonly IRoundRepository _roundRepository;
        private readonly IRankingRepository _rankingRepository;

        public RoundService(IRoundRepository roundRepository,
            IRankingRepository rankingRepository,
            IUnitOfWork unitOfWork,
            INotificationHandler notifications)
            : base(unitOfWork, notifications)
        {
            _roundRepository = roundRepository;
            _rankingRepository = rankingRepository;
        }

        public async Task AddAsync(Round round)
        {
            var rounds = await GetAllAsync();

            if (rounds.Any(x => x.Description == round.Description && x.RankingId == round.RankingId))
                AddNotification("Já existem outra rodada cadastrada com essa descrição.");

            if (await ValidateRoundAsync(round))
            {
                _roundRepository.Add(round);

                if (!await CommitAsync()) 
                    AddNotification("Não foi possível cadastrar a rodada.");
            }
        }

        public async Task UpdateAsync(Guid id, Round round)
        {
            var existingRound = await _roundRepository.GetByIdAsync(id);

            if (existingRound == null) 
                AddNotification("Rodada não encontrada.");

            existingRound.Update(round.Description, round.Date, round.RankingId);
            if (await ValidateRoundAsync(existingRound))
            {
                _roundRepository.Update(existingRound);

                if (!await CommitAsync()) 
                    AddNotification("Não foi possível atualizar a rodada.");
            }
        }

        public async Task<IEnumerable<Round>> GetAllAsync()
        {
            var rounds = await _roundRepository.GetAllAsync();
            return rounds.OrderByDescending(x => x.Created);
        }

        public async Task<Round> GetByIdAsync(Guid id)
        {
            return await _roundRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Round>> GetByRankingIdAsync(Guid rankingId)
        {
            return await _roundRepository.GetByRankingIdAsync(rankingId);
        }

        #region Validate
        private async Task<bool> ValidateRoundAsync(Round round)
        {
            var validateEntity = ValidateEntity(round);
            var validateRanking = await ValidateRankingExistsAsync(round.RankingId);

            return validateEntity && validateRanking;
        }

        private async Task<bool> ValidateRankingExistsAsync(Guid rankingId)
        {
            var ranking = await _rankingRepository.GetByIdAsync(rankingId);
            if (ranking != null) return true;

            AddNotification("Ranking não encontrado.");

            return false;
        }
        #endregion
    }
}
