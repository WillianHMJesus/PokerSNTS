using PokerSNTS.Domain.DTOs;
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

        public async Task<bool> Add(Ranking ranking)
        {
            var validationResult = ranking.Validate();
            if (validationResult.IsValid)
            {
                _rankingRepository.Add(ranking);

                return await _unitOfWork.Commit();
            }

            _notification.HandleNotification(validationResult);

            return false;
        }

        public async Task<bool> Update(Guid id, Ranking ranking)
        {
            var existingRanking = await _rankingRepository.GetById(id);
            if (existingRanking == null) _notification.HandleNotification("DomainValidation", "Ranking não encontrado.");

            if (!_notification.HasNotification())
            {
                existingRanking.Update(ranking.Description, ranking.AwardValue);
                var validationResult = existingRanking.Validate();
                if (validationResult.IsValid)
                {
                    _rankingRepository.Update(existingRanking);

                    return await _unitOfWork.Commit();
                }

                _notification.HandleNotification(validationResult);
            }

            return false;
        }

        public async Task<IEnumerable<RankingDTO>> GetAll()
        {
            var rankingDTO = new List<RankingDTO>();
            var rankings = await _rankingRepository.GetAll();
            foreach (var ranking in rankings)
            {
                rankingDTO.Add(new RankingDTO(ranking));
            }

            return await Task.FromResult(rankingDTO);
        }
    }
}
