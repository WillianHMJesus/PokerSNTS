using PokerSNTS.Domain.Entities;
using PokerSNTS.Domain.Interfaces.Repositories;
using PokerSNTS.Domain.Interfaces.Services;
using PokerSNTS.Domain.Interfaces.UnitOfWork;
using PokerSNTS.Domain.Notifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Services
{
    public class RankingService : IRankingService
    {
        private readonly IRankingRepository _rankingRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDomainNotificationHandler _notification;

        public RankingService(IRankingRepository rankingRepository,
            IUnitOfWork unitOfWork,
            IDomainNotificationHandler notification)
        {
            _rankingRepository = rankingRepository;
            _unitOfWork = unitOfWork;
            _notification = notification;
        }

        public async Task<bool> Add(Ranking ranking)
        {
            if(ranking.IsValid)
            {
                _rankingRepository.Add(ranking);

                return await _unitOfWork.Commit();
            }

            _notification.HandleNotification(ranking.ValidationResult);

            return false;
        }

        public async Task<bool> Update(Ranking ranking)
        {
            if (ranking.IsValid)
            {
                _rankingRepository.Update(ranking);

                return await _unitOfWork.Commit();
            }

            _notification.HandleNotification(ranking.ValidationResult);

            return false;
        }

        public async Task<IEnumerable<Ranking>> GetAll()
        {
            return await _rankingRepository.GetAll();
        }
    }
}
