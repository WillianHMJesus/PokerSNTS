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
    public class RankingPunctuationService : BaseService, IRankingPunctuationService
    {
        private readonly IRankingPunctuationRepository _rankingPunctuationRepository;

        public RankingPunctuationService(IRankingPunctuationRepository rankingPunctuationRepository,
            IUnitOfWork unitOfWork,
            INotificationHandler notifications)
            : base(unitOfWork, notifications)
        {
            _rankingPunctuationRepository = rankingPunctuationRepository;
        }

        public async Task AddAsync(RankingPunctuation rankingPunctuation)
        {
            var rankingPunctuations = await GetAllAsync();

            if (rankingPunctuations.Any(x => x.Position == rankingPunctuation.Position))
                AddNotification("Essa posição do ranking já foi cadastrada anteriormente.");

            if (ValidateEntity(rankingPunctuation))
            {
                _rankingPunctuationRepository.Add(rankingPunctuation);

                if (!await CommitAsync())
                    AddNotification("Não foi possível cadastrar a pontuação do ranking.");
            }
        }

        public async Task UpdateAsync(Guid id, RankingPunctuation rankingPunctuation)
        {
            var existingRankingPunctuation = await _rankingPunctuationRepository.GetByIdAsync(id);

            if (existingRankingPunctuation == null) 
                AddNotification("Pontuação do ranking não foi encontrada.");

            existingRankingPunctuation.Update(rankingPunctuation.Position, rankingPunctuation.Punctuation);
            if (ValidateEntity(existingRankingPunctuation))
            {
                _rankingPunctuationRepository.Update(existingRankingPunctuation);

                if (!await CommitAsync()) 
                    AddNotification("Não foi possível atualizar a pontuação do ranking");
            }
        }

        public async Task<IEnumerable<RankingPunctuation>> GetAllAsync()
        {
            return await _rankingPunctuationRepository.GetAllAsync();
        }

        public async Task<RankingPunctuation> GetByIdAsync(Guid id)
        {
            return await _rankingPunctuationRepository.GetByIdAsync(id);
        }

        public async Task<RankingPunctuation> GetByPositionAsync(short position)
        {
            return await _rankingPunctuationRepository.GetByPositionAsync(position);
        }
    }
}
