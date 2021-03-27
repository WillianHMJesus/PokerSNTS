using PokerSNTS.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Interfaces.Repositories
{
    public interface IRoundRepository
    {
        void Add(Round round);
        void Update(Round round);
        Task<Round> GetRoundByRankingId(Guid rankingId);
    }
}
