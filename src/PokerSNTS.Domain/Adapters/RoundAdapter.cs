using PokerSNTS.Domain.DTOs;
using PokerSNTS.Domain.Entities;

namespace PokerSNTS.Domain.Adapters
{
    public class RoundAdapter
    {
        public static RoundDTO ToRoundDTO(Round round)
        {
            return new RoundDTO(round.Id, round.Description, round.Date, round.RankingId);
        }
    }
}
