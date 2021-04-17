using System.Collections.Generic;

namespace PokerSNTS.Domain.DTOs
{
    public class RankingOverallDTO
    {
        public RankingOverallDTO()
        {
            Players = new List<PlayerRankingDTO>();
        }

        public string Description { get; set; }
        public ICollection<PlayerRankingDTO> Players { get; set; }
    }
}
