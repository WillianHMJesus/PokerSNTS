using PokerSNTS.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Interfaces.Repositories
{
    public interface IRankingPunctuationRepository : IRepository<RankingPunctuation>
    {
        Task<IEnumerable<RankingPunctuation>> GetAllAsync();
        Task<RankingPunctuation> GetRankingPunctuationByPositionAsync(short position);
    }
}
