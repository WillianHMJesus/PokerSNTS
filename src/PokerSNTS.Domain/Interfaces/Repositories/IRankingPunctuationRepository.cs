using PokerSNTS.Domain.Entities;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Interfaces.Repositories
{
    public interface IRankingPunctuationRepository
    {
        void Add(RankingPunctuation rankingPunctuation);
        void Update(RankingPunctuation rankingPunctuation);
        Task<RankingPunctuation> GetRankingPunctuationByPosition(short position);
    }
}
