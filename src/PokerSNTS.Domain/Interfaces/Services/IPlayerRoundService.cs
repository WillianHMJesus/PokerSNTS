using PokerSNTS.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Interfaces.Services
{
    public interface IPlayerRoundService
    {
        Task<bool> Add(PlayerRound playerRound);
        Task<bool> Update(Guid id, PlayerRound playerRound);
    }
}
