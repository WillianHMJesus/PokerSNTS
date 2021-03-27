using PokerSNTS.Domain.Entities;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Interfaces.Services
{
    public interface IPlayerRoundService
    {
        Task<bool> Add(PlayerRound playerRound);
        Task<bool> Update(PlayerRound playerRound);
    }
}
