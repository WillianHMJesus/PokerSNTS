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

        public async Task<bool> AddAsync(Ranking ranking)
        {
            var validationResult = ranking.Validate();
            if (validationResult.IsValid)
            {
                _rankingRepository.Add(ranking);

                return await _unitOfWork.CommitAsync();
            }

            _notification.HandleNotification(validationResult);

            return false;
        }

        public async Task<bool> UpdateAsync(Guid id, Ranking ranking)
        {
            var existingRanking = await _rankingRepository.GetByIdAsync(id);
            if (existingRanking == null) _notification.HandleNotification("DomainValidation", "Ranking não encontrado.");

            if (!_notification.HasNotification())
            {
                existingRanking.Update(ranking.Description, ranking.AwardValue);
                var validationResult = existingRanking.Validate();
                if (validationResult.IsValid)
                {
                    _rankingRepository.Update(existingRanking);

                    return await _unitOfWork.CommitAsync();
                }

                _notification.HandleNotification(validationResult);
            }

            return false;
        }

        public async Task<IEnumerable<Ranking>> GetAllAsync()
        {
            return await _rankingRepository.GetAllAsync();
        }
    }
}
