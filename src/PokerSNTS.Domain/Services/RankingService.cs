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
            var rankings = await GetAllAsync();

            if (rankings.Any(x => x.Description == ranking.Description)) 
                AddNotification("Já existe outro ranking cadastrado com essa descrição.");

            if (ValidateEntity(ranking))
            {
                _rankingRepository.Add(ranking);

                if (!await CommitAsync()) 
                    AddNotification("Não foi possível cadastrar o ranking.");
            }
        }

        public async Task UpdateAsync(Guid id, Ranking ranking)
        {
            var existingRanking = await _rankingRepository.GetByIdAsync(id);

            if (existingRanking == null) 
                AddNotification("Ranking não encontrado.");

            existingRanking.Update(ranking.Description, ranking.AwardValue);
            if (ValidateEntity(existingRanking))
            {
                _rankingRepository.Update(existingRanking);

                if (!await CommitAsync()) 
                    AddNotification("Não foi possível cadastrar o ranking.");
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

        public async Task<Ranking> GetOverallById(Guid id)
        {
            return await _rankingRepository.GetOverallById(id);
        }
    }
}
