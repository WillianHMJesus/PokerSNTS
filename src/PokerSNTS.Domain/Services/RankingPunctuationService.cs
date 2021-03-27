using PokerSNTS.Domain.Entities;
using PokerSNTS.Domain.Interfaces.Repositories;
using PokerSNTS.Domain.Interfaces.Services;
using PokerSNTS.Domain.Interfaces.UnitOfWork;
using PokerSNTS.Domain.Notifications;
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

        public async Task<bool> Add(RankingPunctuation rankingPunctuation)
        {
            if(rankingPunctuation.IsValid)
            {
                _rankingPunctuationRepository.Add(rankingPunctuation);

                return await _unitOfWork.Commit();
            }

            _notification.HandleNotification(rankingPunctuation.ValidationResult);

            return false;
        }

        public async Task<bool> Update(RankingPunctuation rankingPunctuation)
        {
            if (rankingPunctuation.IsValid)
            {
                _rankingPunctuationRepository.Update(rankingPunctuation);

                return await _unitOfWork.Commit();
            }

            _notification.HandleNotification(rankingPunctuation.ValidationResult);

            return false;
        }

        public async Task<RankingPunctuation> GetRankingPunctuationByPosition(short position)
        {
            return await _rankingPunctuationRepository.GetRankingPunctuationByPosition(position);
        }
    }
}
