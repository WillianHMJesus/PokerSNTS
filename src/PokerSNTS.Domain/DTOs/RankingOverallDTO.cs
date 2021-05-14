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
        public string LeaderPlayer => Players?.OrderByDescending(x => x.Points)?.Select(x => x.Name)?.FirstOrDefault();
        public decimal? LeaderValue => AwardValue.HasValue ? Math.Round(AwardValue.Value * 0.5M, 2) : null;
        public string ViceLeaderPlayer => Players?.Where(x => !x.Name.Equals(LeaderPlayer))?.OrderByDescending(x => x.Points)?.Select(x => x.Name)?.FirstOrDefault();
        public decimal? ViceLeaderValue => AwardValue.HasValue ? Math.Round(AwardValue.Value * 0.25M, 2) : null;
        public string AveragePlayer => Players?.Where(x => x.Matches >= (NumberRounds / 2))?.OrderByDescending(x => x.Average)?.ThenBy(x => x.Matches)?.Select(x => x.Name)?.FirstOrDefault();
        public decimal? AverageValue => AwardValue.HasValue ? Math.Round(AwardValue.Value * 0.25M, 2) : null;
        public ICollection<PlayerRankingDTO> Players { get; set; }
    }
}
