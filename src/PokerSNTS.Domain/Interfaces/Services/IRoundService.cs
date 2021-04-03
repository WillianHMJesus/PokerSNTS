using PokerSNTS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Interfaces.Services
{
    public interface IRoundService
    {
        Task<Round> AddAsync(Round round);
        Task<Round> UpdateAsync(Guid id, Round round);
        Task<IEnumerable<Round>> GetRoundByRankingIdAsync(Guid rankingId);
    }
}
