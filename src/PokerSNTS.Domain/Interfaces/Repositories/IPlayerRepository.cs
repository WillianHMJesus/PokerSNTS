using PokerSNTS.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Interfaces.Repositories
{
    public interface IPlayerRepository
    {
        void Add(Player player);
        void Update(Player player);
        Task<IEnumerable<Player>> GetAll();
    }
}
