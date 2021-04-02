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
    public class RankingPunctuationService : IRankingPunctuationService
    {
        private readonly IRankingPunctuationRepository _rankingPunctuationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDomainNotificationHandler _notification;

        public RankingPunctuationService(IRankingPunctuationRepository rankingPunctuationRepository,
            IUnitOfWork unitOfWork,
            IDomainNotificationHandler notification)
        {
            _rankingPunctuationRepository = rankingPunctuationRepository;
            _unitOfWork = unitOfWork;
            _notification = notification;
        }

        public async Task<bool> AddAsync(RankingPunctuation rankingPunctuation)
        {
            var validationResult = rankingPunctuation.Validate();
            if (validationResult.IsValid)
            {
                _rankingPunctuationRepository.Add(rankingPunctuation);

                return await _unitOfWork.CommitAsync();
            }

            _notification.HandleNotification(validationResult);

            return false;
        }

        public async Task<bool> UpdateAsync(Guid id, RankingPunctuation rankingPunctuation)
        {
            var existingRankingPunctuation = await _rankingPunctuationRepository.GetByIdAsync(id);
            if (existingRankingPunctuation == null) _notification.HandleNotification("DomainValidation", "Pontuação do ranking não foi encontrada.");

            if(!_notification.HasNotification())
            {
                existingRankingPunctuation.Update(rankingPunctuation.Position, rankingPunctuation.Punctuation);
                var validationResult = existingRankingPunctuation.Validate();
                if (validationResult.IsValid)
                {
                    _rankingPunctuationRepository.Update(existingRankingPunctuation);

                    return await _unitOfWork.CommitAsync();
                }

                _notification.HandleNotification(validationResult);
            }

            return false;
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
