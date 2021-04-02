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
        private readonly IRankingRepository _rankingRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDomainNotificationHandler _notification;

        public RoundService(IRoundRepository roundRepository,
            IRankingRepository rankingRepository,
            IUnitOfWork unitOfWork,
            IDomainNotificationHandler notification)
        {
            _roundRepository = roundRepository;
            _rankingRepository = rankingRepository;
            _unitOfWork = unitOfWork;
            _notification = notification;
        }

        public async Task<bool> AddAsync(Round round)
        {
            var validationResult = round.Validate();
            if(validationResult.IsValid)
            {
                if(await ValidateRankingExistsAsync(round.RankingId))
                {
                    _roundRepository.Add(round);

                    return await _unitOfWork.CommitAsync();
                }
            }

            _notification.HandleNotification(validationResult);

            return false;
        }

        public async Task<bool> UpdateAsync(Guid id, Round round)
        {
            var existingRound = await _roundRepository.GetByIdAsync(id);
            if (existingRound == null) _notification.HandleNotification("DomainValidation", "Rodada não encontrada.");

            if(!_notification.HasNotification())
            {
                existingRound.Update(round.Description, round.Date, round.RankingId);
                var validationResult = existingRound.Validate();
                if (validationResult.IsValid)
                {
                    if (await ValidateRankingExistsAsync(round.RankingId))
                    {
                        _roundRepository.Update(existingRound);

                        return await _unitOfWork.CommitAsync();
                    }
                }

                _notification.HandleNotification(validationResult);
            }

            return false;
        }

        public async Task<IEnumerable<Round>> GetRoundByRankingIdAsync(Guid rankingId)
        {
            return await _roundRepository.GetRoundByRankingIdAsync(rankingId);
        }

        private async Task<bool> ValidateRankingExistsAsync(Guid rankingId)
        {
            var ranking = await _rankingRepository.GetByIdAsync(rankingId);
            if (ranking == null)
            {
                _notification.HandleNotification("DomainValidation", "Ranking não encontrado.");

                return false;
            }

            return true;
        }
    }
}
