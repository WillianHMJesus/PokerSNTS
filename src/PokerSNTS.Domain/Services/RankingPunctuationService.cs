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
    public class RankingPointService : BaseService, IRankingPointService
    {
        private readonly IRankingPointRepository _rankingPointRepository;

        public RankingPointService(IRankingPointRepository rankingPointRepository,
            IUnitOfWork unitOfWork,
            INotificationHandler notifications)
            : base(unitOfWork, notifications)
        {
            _rankingPointRepository = rankingPointRepository;
        }

        public async Task AddAsync(RankingPoint rankingPoint)
        {
            var rankingPoints = await GetAllAsync();

            if (rankingPoints.Any(x => x.Position == rankingPoint.Position))
                AddNotification("Essa posição do ranking já foi cadastrada anteriormente.");

            if (ValidateEntity(rankingPoint))
            {
                _rankingPointRepository.Add(rankingPoint);

                if (!await CommitAsync())
                    AddNotification("Não foi possível cadastrar a pontuação do ranking.");
            }
        }

        public async Task UpdateAsync(Guid id, RankingPoint rankingPoint)
        {
            var existingRankingPoint = await _rankingPointRepository.GetByIdAsync(id);

            if (existingRankingPoint == null) 
                AddNotification("Pontuação do ranking não foi encontrada.");

            existingRankingPoint.Update(rankingPoint.Position, rankingPoint.Point);
            if (ValidateEntity(existingRankingPoint))
            {
                _rankingPointRepository.Update(existingRankingPoint);

                if (!await CommitAsync()) 
                    AddNotification("Não foi possível atualizar a pontuação do ranking");
            }
        }

        public async Task<IEnumerable<RankingPoint>> GetAllAsync()
        {
            return await _rankingPointRepository.GetAllAsync();
        }

        public async Task<RankingPoint> GetByIdAsync(Guid id)
        {
            return await _rankingPointRepository.GetByIdAsync(id);
        }

        public async Task<RankingPoint> GetByPositionAsync(short position)
        {
            return await _rankingPointRepository.GetByPositionAsync(position);
        }
    }
}
