using PokerSNTS.Domain.DTOs;
using PokerSNTS.Domain.Entities;

namespace PokerSNTS.Domain.Adapters
{
    public class RankingPointAdapter
    {
        public static RankingPointDTO ToRankingPointDTO(RankingPoint rankingPoint)
        {
            return new RankingPointDTO(rankingPoint.Id, rankingPoint.Position, rankingPoint.Point);
        }
    }
}
