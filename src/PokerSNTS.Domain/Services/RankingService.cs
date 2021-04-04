using PokerSNTS.Domain.DTOs;
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
    public class RankingService : BaseService, IRankingService
    {
        private readonly IRankingRepository _rankingRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDomainNotificationHandler _notifications;

        public RankingService(IRankingRepository rankingRepository,
            IUnitOfWork unitOfWork,
            IDomainNotificationHandler notifications)
            : base(notifications)
        {
            _rankingRepository = rankingRepository;
            _unitOfWork = unitOfWork;
            _notifications = notifications;
        }

        public async Task<Ranking> AddAsync(Ranking ranking)
        {
            if (ValidateEntity(ranking))
            {
                _rankingRepository.Add(ranking);
                if (await _unitOfWork.CommitAsync()) return ranking;
            }

            return null;
        }

        public async Task<Ranking> UpdateAsync(Guid id, Ranking ranking)
        {
            var existingRanking = await _rankingRepository.GetByIdAsync(id);
            if (existingRanking == null)
            {
                _notifications.HandleNotification("DomainValidation", "Ranking não encontrado.");
                return null;
            }

            existingRanking.Update(ranking.Description, ranking.AwardValue);
            if (ValidateEntity(existingRanking))
            {
                _rankingRepository.Update(existingRanking);
                if (await _unitOfWork.CommitAsync()) return existingRanking;
            }

            return null;
        }

        public async Task<IEnumerable<Ranking>> GetAllAsync()
        {
            return await _rankingRepository.GetAllAsync();
        }

        public async Task<IEnumerable<RankingOverallDTO>> GetOverallById(Guid id)
        {
            return await _rankingRepository.GetOverallById(id);
        }

        public async Task<IEnumerable<RankingOverallDTO>> GetOverallByPeriod(DateTime initialDate, DateTime finalDate)
        {
            return await _rankingRepository.GetOverallByPeriod(initialDate, finalDate);
        }
    }
}
