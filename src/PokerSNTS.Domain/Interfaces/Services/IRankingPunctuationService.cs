using PokerSNTS.Domain.Entities;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Interfaces.Services
{
    public interface IRankingPunctuationService
    {
        Task<bool> Add(RankingPunctuation rankingPunctuation);
        Task<bool> Update(RankingPunctuation rankingPunctuation);
        Task<RankingPunctuation> GetRankingPunctuationByPosition(short position);
    }
}
