using PokerSNTS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Interfaces.Repositories
{
    public interface IRoundRepository : IRepository<Round>
    {
        Task<IEnumerable<Round>> GetByRankingIdAsync(Guid rankingId);
    }
}
