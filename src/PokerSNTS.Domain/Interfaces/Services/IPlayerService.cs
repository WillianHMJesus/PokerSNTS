using PokerSNTS.Domain.DTOs;
using PokerSNTS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Interfaces.Services
{
    public interface IPlayerService
    {
        Task<bool> Add(Player player);
        Task<bool> Update(Guid id, Player player);
        Task<IEnumerable<PlayerDTO>> GetAll();
    }
}
