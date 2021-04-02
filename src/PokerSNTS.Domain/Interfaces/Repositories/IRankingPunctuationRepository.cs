using PokerSNTS.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Interfaces.Repositories
{
    public interface IRankingPunctuationRepository : IRepository<RankingPunctuation>
    {
        Task<RankingPunctuation> GetRankingPunctuationByPosition(short position);
        Task<IEnumerable<RankingPunctuation>> GetAll();
    }
}
