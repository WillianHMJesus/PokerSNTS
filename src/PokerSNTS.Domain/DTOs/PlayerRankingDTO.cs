using System;

namespace PokerSNTS.Domain.DTOs
{
    public class PlayerRankingDTO
    {
        public string Name { get; set; }
        public short Points { get; set; }
        public short Matches { get; set; }
        public double Average => Math.Round((float)Points / Matches, 1);
    }
}
