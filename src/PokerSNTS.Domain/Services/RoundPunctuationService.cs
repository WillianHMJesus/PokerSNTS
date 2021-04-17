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
    public class RoundPointService : BaseService, IRoundPointService
    {
        private readonly IRoundPointRepository _roundPointRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IRoundRepository _roundRepository;

        public RoundPointService(IRoundPointRepository roundPointRepository,
            IPlayerRepository playerRepository,
            IRoundRepository roundRepository,
            IUnitOfWork unitOfWork,
            INotificationHandler notifications)
            : base(unitOfWork, notifications)
        {
            _roundPointRepository = roundPointRepository;
            _playerRepository = playerRepository;
            _roundRepository = roundRepository;
        }

        public async Task AddAsync(RoundPoint roundPoint)
        {
            var roundsPoints = await GetAllAsync();

            if (roundsPoints.Any(x => x.PlayerId == roundPoint.PlayerId && x.RoundId == roundPoint.RoundId))
                AddNotification("Esse jogador já tem posição para essa rodada.");

            if (await ValidateRoundPointAsync(roundPoint))
            {
                _roundPointRepository.Add(roundPoint);

                if (!await CommitAsync())
                    AddNotification("Não foi possível cadastrar a pontuação da rodada.");
            }
        }

        public async Task UpdateAsync(Guid id, RoundPoint roundPoint)
        {
            var existingRoundPoint = await _roundPointRepository.GetByIdAsync(id);

            if (existingRoundPoint == null) 
                AddNotification("Partida do jogador não encontrada.");

            existingRoundPoint.Update(
                roundPoint.Position, roundPoint.Point, roundPoint.PlayerId, roundPoint.RoundId);
            if (await ValidateRoundPointAsync(existingRoundPoint))
            {
                _roundPointRepository.Update(existingRoundPoint);

                if (!await CommitAsync()) 
                    AddNotification("Não foi possível atualizar a pontuação da rodada.");
            }
        }

        public async Task<IEnumerable<RoundPoint>> GetAllAsync()
        {
            return await _roundPointRepository.GetAllAsync();
        }

        public async Task<RoundPoint> GetByIdAsync(Guid id)
        {
            return await _roundPointRepository.GetByIdAsync(id);
        }

        #region Validate
        private async Task<bool> ValidateRoundPointAsync(RoundPoint roundPoint)
        {
            var validationEntity = ValidateEntity(roundPoint);
            var validationPlayer = await ValidatePlayerExistsAsync(roundPoint.PlayerId);
            var validationRound = await ValidateRoundExistsAsync(roundPoint.RoundId);

            return validationEntity && validationPlayer && validationRound;
        }

        private async Task<bool> ValidatePlayerExistsAsync(Guid playerId)
        {
            var player = await _playerRepository.GetByIdAsync(playerId);
            if (player != null) return true;

            AddNotification("Jogador não encontrado.");

            return false;
        }

        private async Task<bool> ValidateRoundExistsAsync(Guid roundId)
        {
            var round = await _roundRepository.GetByIdAsync(roundId);
            if (round != null) return true;

            AddNotification("Rodada não encontrada.");

            return false;
        }
        #endregion
    }
}
