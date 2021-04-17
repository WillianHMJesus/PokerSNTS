using PokerSNTS.Domain.DTOs;
using PokerSNTS.Domain.Entities;

namespace PokerSNTS.Domain.Adapters
{
    public class RoundPointAdapter
    {
        public static RoundPointDTO ToRoundPointDTO(RoundPoint roundPoint)
        {
            return new RoundPointDTO(roundPoint.Id, roundPoint.Position, roundPoint.Point, roundPoint.PlayerId, roundPoint.RoundId);
        }
    }
}
