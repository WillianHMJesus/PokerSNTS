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
    public class RankingPunctuationService : BaseService, IRankingPunctuationService
    {
        private readonly IRankingPunctuationRepository _rankingPunctuationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDomainNotificationHandler _notifications;

        public RankingPunctuationService(IRankingPunctuationRepository rankingPunctuationRepository,
            IUnitOfWork unitOfWork,
            IDomainNotificationHandler notifications)
            : base(notifications)
        {
            _rankingPunctuationRepository = rankingPunctuationRepository;
            _unitOfWork = unitOfWork;
            _notifications = notifications;
        }

        public async Task<RankingPunctuation> AddAsync(RankingPunctuation rankingPunctuation)
        {
            if(ValidateEntity(rankingPunctuation))
            {
                _rankingPunctuationRepository.Add(rankingPunctuation);
                if (await _unitOfWork.CommitAsync()) return rankingPunctuation;
            }

            return null;
        }

        public async Task<RankingPunctuation> UpdateAsync(Guid id, RankingPunctuation rankingPunctuation)
        {
            var existingRankingPunctuation = await _rankingPunctuationRepository.GetByIdAsync(id);
            if (existingRankingPunctuation == null)
            {
                _notifications.HandleNotification("DomainValidation", "Pontuação do ranking não foi encontrada.");
                return null;
            }

            existingRankingPunctuation.Update(rankingPunctuation.Position, rankingPunctuation.Punctuation);
            if(ValidateEntity(existingRankingPunctuation))
            {
                _rankingPunctuationRepository.Update(existingRankingPunctuation);
                if (await _unitOfWork.CommitAsync()) return existingRankingPunctuation;
            }

            return null;
        }

        public async Task<IEnumerable<RankingPunctuation>> GetAllAsync()
        {
            return await _rankingPunctuationRepository.GetAllAsync();
        }

        public async Task<RankingPunctuation> GetRankingPunctuationByPositionAsync(short position)
        {
            return await _rankingPunctuationRepository.GetRankingPunctuationByPositionAsync(position);
        }
    }
}
