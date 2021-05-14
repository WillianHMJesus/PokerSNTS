using PokerSNTS.Domain.DTOs;
using PokerSNTS.Domain.Entities;
using System.Linq;

namespace PokerSNTS.Domain.Adapters
{
    public class RankingAdapter
    {
        public static RankingDTO ToRankingDTO(Ranking ranking)
        {
            return new RankingDTO(ranking.Id, ranking.Description, ranking.AwardValue);
        }

        public static RankingOverallDTO ToRankingOverallDTO(Ranking ranking)
        {
            var rankingOverallDTO = new RankingOverallDTO()
            { 
                Description = ranking.Description,
                AwardValue = ranking.AwardValue,
                NumberRounds = ranking.Rounds.Count
            };

            foreach (var roundPoint in ranking.Rounds.SelectMany(x => x.RoundsPoints))
            {
                var playerRanking = rankingOverallDTO.Players.FirstOrDefault(x => x.Name == roundPoint.Player.Name);
                if (playerRanking == null)
                {
                    playerRanking = new PlayerRankingDTO() { Name = roundPoint.Player.Name };
                    rankingOverallDTO.Players.Add(playerRanking);
                }

                playerRanking.Points += roundPoint.Point;
                playerRanking.Matches++;
            }

            rankingOverallDTO.Players = rankingOverallDTO.Players.OrderByDescending(x => x.Points).ToList();

            return rankingOverallDTO;
        }
    }
}
