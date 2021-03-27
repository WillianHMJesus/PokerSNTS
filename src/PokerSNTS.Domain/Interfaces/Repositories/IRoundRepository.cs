using PokerSNTS.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Interfaces.Repositories
{
    public interface IRoundRepository : IRepository<Round>
    {
        Task<Round> GetRoundByRankingId(Guid rankingId);
    }
}
