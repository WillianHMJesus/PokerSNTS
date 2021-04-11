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

        public RankingService(IRankingRepository rankingRepository,
            IUnitOfWork unitOfWork,
            INotificationHandler notifications)
            : base(unitOfWork, notifications)
        {
            _rankingRepository = rankingRepository;
        }

        public async Task AddAsync(Ranking ranking)
        {
            if (ValidateEntity(ranking))
            {
                _rankingRepository.Add(ranking);

                if (!await CommitAsync()) AddNotification("Não foi possível cadastrar o ranking.");
            }
        }

        public async Task UpdateAsync(Guid id, Ranking ranking)
        {
            var existingRanking = await _rankingRepository.GetByIdAsync(id);
            if (existingRanking == null) AddNotification("Ranking não encontrado.");

            existingRanking.Update(ranking.Description, ranking.AwardValue);
            if (ValidateEntity(existingRanking))
            {
                _rankingRepository.Update(existingRanking);

                if (!await CommitAsync()) AddNotification("Não foi possível cadastrar o ranking.");
            }
        }

        public async Task<IEnumerable<Ranking>> GetAllAsync()
        {
            return await _rankingRepository.GetAllAsync();
        }

        public async Task<Ranking> GetByIdAsync(Guid id)
        {
            return await _rankingRepository.GetByIdAsync(id);
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
