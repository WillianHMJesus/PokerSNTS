using PokerSNTS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Interfaces.Services
{
    public interface IRankingPointService
    {
        Task AddAsync(RankingPoint rankingPoint);
        Task UpdateAsync(Guid id, RankingPoint rankingPoint);
        Task<IEnumerable<RankingPoint>> GetAllAsync();
        Task<RankingPoint> GetByIdAsync(Guid id);
        Task<RankingPoint> GetByPositionAsync(short position);
    }
}
