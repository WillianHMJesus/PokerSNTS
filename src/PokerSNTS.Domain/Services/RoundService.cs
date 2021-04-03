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
    public class RoundService : BaseService, IRoundService
    {
        private readonly IRoundRepository _roundRepository;
        private readonly IRankingRepository _rankingRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDomainNotificationHandler _notifications;

        public RoundService(IRoundRepository roundRepository,
            IRankingRepository rankingRepository,
            IUnitOfWork unitOfWork,
            IDomainNotificationHandler notifications)
            : base(notifications)
        {
            _roundRepository = roundRepository;
            _rankingRepository = rankingRepository;
            _unitOfWork = unitOfWork;
            _notifications = notifications;
        }

        public async Task<Round> AddAsync(Round round)
        {
            if (await ValidateRoundAsync(round))
            {
                _roundRepository.Add(round);
                if (await _unitOfWork.CommitAsync()) return round;
            }

            return null;
        }

        public async Task<Round> UpdateAsync(Guid id, Round round)
        {
            var existingRound = await _roundRepository.GetByIdAsync(id);
            if (existingRound == null)
            {
                _notifications.HandleNotification("DomainValidation", "Rodada não encontrada.");
            }

            existingRound.Update(round.Description, round.Date, round.RankingId);
            if (await ValidateRoundAsync(existingRound))
            {
                _roundRepository.Update(existingRound);
                if (await _unitOfWork.CommitAsync()) return round;
            }

            return null;
        }

        public async Task<IEnumerable<Round>> GetRoundByRankingIdAsync(Guid rankingId)
        {
            return await _roundRepository.GetRoundByRankingIdAsync(rankingId);
        }

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

            _notifications.HandleNotification("DomainValidation", "Ranking não encontrado.");

            return false;
        }
    }
}
