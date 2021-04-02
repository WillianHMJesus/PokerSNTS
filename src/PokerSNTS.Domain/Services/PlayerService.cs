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
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDomainNotificationHandler _notification;

        public PlayerService(IPlayerRepository playerRepository, 
            IUnitOfWork unitOfWork, 
            IDomainNotificationHandler notification)
        {
            _playerRepository = playerRepository;
            _unitOfWork = unitOfWork;
            _notification = notification;
        }

        public async Task<bool> Add(Player player)
        {
            var validationResult = player.Validate();
            if (validationResult.IsValid)
            {
                _playerRepository.Add(player);

                return await _unitOfWork.Commit();
            }

            _notification.HandleNotification(validationResult);

            return false;
        }

        public async Task<bool> Update(Guid id, Player player)
        {
            var existingPlayer = await _playerRepository.GetById(id);
            if (existingPlayer == null) _notification.HandleNotification("DomainValidation", "Jogador não encontrado.");

            if(!_notification.HasNotification())
            {
                existingPlayer.Update(player.Name);
                var validationResult = existingPlayer.Validate();
                if (validationResult.IsValid)
                {
                    _playerRepository.Update(existingPlayer);

                    return await _unitOfWork.Commit();
                }

                _notification.HandleNotification(validationResult);
            }

            return false;
        }

        public async Task<IEnumerable<PlayerDTO>> GetAll()
        {
            var playersDTO = new List<PlayerDTO>();
            var players = await _playerRepository.GetAll();
            foreach (var player in players)
            {
                playersDTO.Add(new PlayerDTO(player));
            }

            return await Task.FromResult(playersDTO);
        }
    }
}
