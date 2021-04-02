using PokerSNTS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Interfaces.Services
{
    public interface IPlayerService
    {
        Task<bool> AddAsync(Player player);
        Task<bool> UpdateAsync(Guid id, Player player);
        Task<IEnumerable<Player>> GetAllAsync();
    }
}
