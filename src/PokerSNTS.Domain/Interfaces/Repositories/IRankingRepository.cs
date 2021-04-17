using PokerSNTS.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Interfaces.Repositories
{
    public interface IRankingRepository : IRepository<Ranking>
    {
        Task<Ranking> GetOverallById(Guid id);
    }
}
