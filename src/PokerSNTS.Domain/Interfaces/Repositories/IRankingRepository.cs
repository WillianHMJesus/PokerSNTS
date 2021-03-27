using PokerSNTS.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Interfaces.Repositories
{
    public interface IRankingRepository : IRepository<Ranking>
    {
        Task<IEnumerable<Ranking>> GetAll();
    }
}
