using PokerSNTS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Interfaces.Services
{
    public interface IRoundService
    {
        Task<bool> AddAsync(Round round);
        Task<bool> UpdateAsync(Guid id, Round round);
        Task<IEnumerable<Round>> GetRoundByRankingIdAsync(Guid rankingId);
    }
}
