using PokerSNTS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Interfaces.Services
{
    public interface IPlayerService
    {
        Task<Player> AddAsync(Player player);
        Task<Player> UpdateAsync(Guid id, Player player);
        Task<IEnumerable<Player>> GetAllAsync();
    }
}
