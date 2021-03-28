using PokerSNTS.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Interfaces.Services
{
    public interface IRankingPunctuationService
    {
        Task<bool> Add(RankingPunctuation rankingPunctuation);
        Task<bool> Update(Guid id, RankingPunctuation rankingPunctuation);
        Task<RankingPunctuation> GetRankingPunctuationByPosition(short position);
    }
}
