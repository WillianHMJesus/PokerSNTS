using PokerSNTS.Domain.Entities;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Interfaces.Repositories
{
    public interface IRankingPunctuationRepository : IRepository<RankingPunctuation>
    {
        Task<RankingPunctuation> GetRankingPunctuationByPosition(short position);
    }
}
