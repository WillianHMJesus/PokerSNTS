using PokerSNTS.Domain.DTOs;
using PokerSNTS.Domain.Entities;

namespace PokerSNTS.Domain.Adapters
{
    public class RankingAdapter
    {
        public static RankingDTO ToRankingDTO(Ranking ranking)
        {
            return new RankingDTO(ranking.Id, ranking.Description, ranking.AwardValue);
        }
    }
}
