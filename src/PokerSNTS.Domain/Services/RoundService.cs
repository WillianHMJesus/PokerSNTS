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

        public async Task<bool> Add(Round round)
        {
            var validationResult = round.Validate();
            if(validationResult.IsValid)
            {
                if(await ValidateRankingExists(round.RankingId))
                {
                    _roundRepository.Add(round);

                    return await _unitOfWork.Commit();
                }
            }

            _notification.HandleNotification(validationResult);

            return false;
        }

        public async Task<bool> Update(Guid id, Round round)
        {
            var existingRound = await _roundRepository.GetById(id);
            if (existingRound == null) _notification.HandleNotification("DomainValidation", "Rodada não encontrada.");

            if(!_notification.HasNotification())
            {
                existingRound.Update(round.Description, round.Date, round.RankingId);
                var validationResult = existingRound.Validate();
                if (validationResult.IsValid)
                {
                    if (await ValidateRankingExists(round.RankingId))
                    {
                        _roundRepository.Update(existingRound);

                        return await _unitOfWork.Commit();
                    }
                }

                _notification.HandleNotification(validationResult);
            }

            return false;
        }

        public async Task<IEnumerable<RoundDTO>> GetRoundByRankingId(Guid rankingId)
        {
            var roundsDTO = new List<RoundDTO>();
            var rounds = await _roundRepository.GetRoundByRankingId(rankingId);
            foreach (var round in rounds)
            {
                roundsDTO.Add(new RoundDTO(round));
            }

            return await Task.FromResult(roundsDTO);
        }

        private async Task<bool> ValidateRankingExists(Guid rankingId)
        {
            var ranking = await _rankingRepository.GetById(rankingId);
            if (ranking == null)
            {
                _notification.HandleNotification("DomainValidation", "Ranking não encontrado.");

                return false;
            }

            return true;
        }
    }
}
