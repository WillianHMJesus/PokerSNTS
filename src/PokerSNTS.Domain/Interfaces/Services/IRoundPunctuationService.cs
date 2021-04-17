using PokerSNTS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Interfaces.Services
{
    public interface IRoundPointService
    {
        Task AddAsync(RoundPoint roundPoint);
        Task UpdateAsync(Guid id, RoundPoint roundPoint);
        Task<IEnumerable<RoundPoint>> GetAllAsync();
        Task<RoundPoint> GetByIdAsync(Guid id);
    }
}
