using PokerSNTS.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Interfaces.Services
{
    public interface IPlayerService
    {
        Task<bool> Add(Player player);
        Task<bool> Update(Player player);
        Task<IEnumerable<Player>> GetAll();
    }
}
