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
    public class PlayerService : BaseService, IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository playerRepository,
            IUnitOfWork unitOfWork,
            INotificationHandler notifications)
            : base(unitOfWork, notifications)
        {
            _playerRepository = playerRepository;
        }

        public async Task AddAsync(Player player)
        {
            var players = await GetAllAsync();

            if(players.Any(x => x.Name == player.Name)) 
                AddNotification("Já existe outro jogador cadastrado com esse nome.");

            if (ValidateEntity(player))
            {
                _playerRepository.Add(player);

                if (!await CommitAsync()) 
                    AddNotification("Não foi possível cadastrar o jogador.");
            }
        }

        public async Task UpdateAsync(Guid id, Player player)
        {
            var existingPlayer = await _playerRepository.GetByIdAsync(id);

            if (existingPlayer == null) 
                AddNotification("Jogador não encontrado.");

            existingPlayer.Update(player.Name);
            if (ValidateEntity(existingPlayer))
            {
                _playerRepository.Update(existingPlayer);

                if (!await CommitAsync()) 
                    AddNotification("Não foi possível atualizar o jogador.");
            }
        }

        public async Task<IEnumerable<Player>> GetAllAsync()
        {
            return await _playerRepository.GetAllAsync();
        }

        public async Task<Player> GetByIdAsync(Guid id)
        {
            return await _playerRepository.GetByIdAsync(id);
        }
    }
}
