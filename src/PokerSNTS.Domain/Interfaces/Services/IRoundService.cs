using PokerSNTS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Interfaces.Services
{
    public interface IRoundService
    {
        Task<bool> Add(Round round);
        Task<bool> Update(Round round);
        Task<IEnumerable<Round>> GetRoundByRankingId(Guid rankingId);
    }
}
