using PokerSNTS.Domain.Entities;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Interfaces.Repositories
{
    public interface IRankingPointRepository : IRepository<RankingPoint>
    {
        Task<RankingPoint> GetByPositionAsync(short position);
    }
}
