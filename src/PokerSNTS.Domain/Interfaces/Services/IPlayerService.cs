using PokerSNTS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Interfaces.Services
{
    public interface IPlayerService
    {
        Task AddAsync(Player player);
        Task UpdateAsync(Guid id, Player player);
        Task<IEnumerable<Player>> GetAllAsync();
        Task<Player> GetByIdAsync(Guid id);
    }
}
