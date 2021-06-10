using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerSNTS.Domain.DTOs
{
    public class RankingOverallDTO
    {
        public RankingOverallDTO()
        {
            Players = new List<PlayerRankingDTO>();
        }

        public string Description { get; set; }
        public decimal? AwardValue { get; set; }
        public int NumberRounds { get; set; }
        public string LeaderPlayer => GetFirstPlacePlayer();
        public decimal LeaderValue => GetFirstPlaceValue();
        public string ViceLeaderPlayer => GetSecondPlacePlayer();
        public decimal ViceLeaderValue => GetSecondPlaceValue();
        public string AveragePlayer => GetThirdPlacePlayer();
        public decimal AverageValue => GetThirdPlaceValue();
        public ICollection<PlayerRankingDTO> Players { get; set; }

        private string GetFirstPlacePlayer()
        {
            return Players
                ?.OrderByDescending(x => x.Points)
                ?.Select(x => x.Name)
                ?.FirstOrDefault();
        }

        private string GetSecondPlacePlayer()
        {
            return Players
                ?.Where(x => !x.Name.Equals(LeaderPlayer))
                ?.OrderByDescending(x => x.Points)
                ?.Select(x => x.Name)
                ?.FirstOrDefault();
        }

        private string GetThirdPlacePlayer()
        {
            return Players
                ?.Where(x => !x.Name.Equals(LeaderPlayer) && !x.Name.Equals(ViceLeaderPlayer))
                ?.OrderByDescending(x => x.Points)
                ?.Select(x => x.Name)
                ?.FirstOrDefault();
        }

        private decimal GetFirstPlaceValue()
        {
            return AwardValue.HasValue ? 
                Math.Round(AwardValue.Value * 0.5M, 2) : 0;
        }

        private decimal GetSecondPlaceValue()
        {
            return AwardValue.HasValue ? 
                Math.Round(AwardValue.Value * 0.35M, 2) : 0;
        }

        private decimal GetThirdPlaceValue()
        {
            return AwardValue.HasValue ? 
                Math.Round(AwardValue.Value * 0.15M, 2) : 0;
        }
    }
}
