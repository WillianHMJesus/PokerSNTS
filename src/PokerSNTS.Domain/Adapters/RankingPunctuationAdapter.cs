using PokerSNTS.Domain.DTOs;
using PokerSNTS.Domain.Entities;

namespace PokerSNTS.Domain.Adapters
{
    public class RankingPunctuationAdapter
    {
        public static RankingPunctuationDTO ToRankingPunctuationDTO(RankingPunctuation rankingPunctuation)
        {
            return new RankingPunctuationDTO(rankingPunctuation.Id, rankingPunctuation.Position, rankingPunctuation.Punctuation);
        }
    }
}
