using PokerSNTS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Interfaces.Services
{
    public interface IRankingService
    {
        Task<bool> AddAsync(Ranking ranking);
        Task<bool> UpdateAsync(Guid id, Ranking ranking);
        Task<IEnumerable<Ranking>> GetAllAsync();
    }
}
