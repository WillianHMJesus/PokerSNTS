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
        public string LeaderPlayer => GetLeaderPlayer();
        public decimal LeaderValue => GetLeaderValue();
        public string ViceLeaderPlayer => GetViceLeaderPlayer();
        public decimal ViceLeaderValue => GetViceLeaderValue();
        public string AveragePlayer => GetAveragePlayer();
        public decimal AverageValue => GetAverageValue();
        public ICollection<PlayerRankingDTO> Players { get; set; }

        private string GetLeaderPlayer()
        {
            return Players
                ?.OrderByDescending(x => x.Points)
                ?.Select(x => x.Name)
                ?.FirstOrDefault();
        }

        private string GetViceLeaderPlayer()
        {
            return Players
                ?.Where(x => !x.Name.Equals(LeaderPlayer))
                ?.OrderByDescending(x => x.Points)
                ?.Select(x => x.Name)
                ?.FirstOrDefault();
        }

        private string GetAveragePlayer()
        {
            return Players
                ?.Where(x => x.Matches >= (NumberRounds / 2))
                ?.OrderByDescending(x => x.Average)
                ?.ThenByDescending(x => x.Points)
                ?.ThenByDescending(x => x.Matches)
                ?.Select(x => x.Name)
                ?.FirstOrDefault();
        }

        private decimal GetLeaderValue()
        {
            return AwardValue.HasValue ? 
                Math.Round(AwardValue.Value * 0.5M, 2) : 0;
        }

        private decimal GetViceLeaderValue()
        {
            return AwardValue.HasValue ? 
                Math.Round(AwardValue.Value * 0.25M, 2) : 0;
        }

        private decimal GetAverageValue()
        {
            return AwardValue.HasValue ? 
                Math.Round(AwardValue.Value * 0.25M, 2) : 0;
        }
    }
}
