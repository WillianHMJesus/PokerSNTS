using PokerSNTS.Domain.DTOs;
using PokerSNTS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Interfaces.Repositories
{
    public interface IRankingRepository : IRepository<Ranking>
    {
        Task<IEnumerable<Ranking>> GetAllAsync();
        Task<IEnumerable<RankingOverallDTO>> GetOverallById(Guid id);
        Task<IEnumerable<RankingOverallDTO>> GetOverallByPeriod(DateTime initialDate, DateTime finalDate);
    }
}
