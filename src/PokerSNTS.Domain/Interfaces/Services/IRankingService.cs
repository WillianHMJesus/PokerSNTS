using PokerSNTS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Interfaces.Services
{
    public interface IRankingService
    {
        Task<Ranking> AddAsync(Ranking ranking);
        Task<Ranking> UpdateAsync(Guid id, Ranking ranking);
        Task<IEnumerable<Ranking>> GetAllAsync();
    }
}
