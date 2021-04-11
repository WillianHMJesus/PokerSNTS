using PokerSNTS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Interfaces.Services
{
    public interface IRoundService
    {
        Task AddAsync(Round round);
        Task UpdateAsync(Guid id, Round round);
        Task<IEnumerable<Round>> GetAllAsync();
        Task<Round> GetByIdAsync(Guid id);
        Task<IEnumerable<Round>> GetByRankingIdAsync(Guid rankingId);
    }
}
