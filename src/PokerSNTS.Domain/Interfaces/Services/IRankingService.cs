using PokerSNTS.Domain.DTOs;
using PokerSNTS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Interfaces.Services
{
    public interface IRankingService
    {
        Task AddAsync(Ranking ranking);
        Task UpdateAsync(Guid id, Ranking ranking);
        Task<IEnumerable<Ranking>> GetAllAsync();
        Task<Ranking> GetByIdAsync(Guid id);
        Task<IEnumerable<RankingOverallDTO>> GetOverallById(Guid id);
        Task<IEnumerable<RankingOverallDTO>> GetOverallByPeriod(DateTime initialDate, DateTime finalDate);
    }
}
