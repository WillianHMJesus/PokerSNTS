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
    public class RoundService : IRoundService
    {
        private readonly IRoundRepository _roundRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDomainNotificationHandler _notification;

        public RoundService(IRoundRepository roundRepository,
            IUnitOfWork unitOfWork,
            IDomainNotificationHandler notification)
        {
            _roundRepository = roundRepository;
            _unitOfWork = unitOfWork;
            _notification = notification;
        }

        public async Task<bool> Add(Round round)
        {
            if(round.IsValid)
            {
                _roundRepository.Add(round);

                return await _unitOfWork.Commit();
            }

            _notification.HandleNotification(round.ValidationResult);

            return false;
        }

        public async Task<bool> Update(Round round)
        {
            if (round.IsValid)
            {
                _roundRepository.Update(round);

                return await _unitOfWork.Commit();
            }

            _notification.HandleNotification(round.ValidationResult);

            return false;
        }

        public async Task<IEnumerable<Round>> GetRoundByRankingId(Guid rankingId)
        {
            return await _roundRepository.GetRoundByRankingId(rankingId);
        }
    }
}
