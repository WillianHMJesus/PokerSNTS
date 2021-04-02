using System;

namespace PokerSNTS.Domain.DTOs
{
    public class RankingDTO
    {
        public RankingDTO(Guid id, string description, decimal? awardValue)
        {
            Id = id;
            Description = description;
            AwardValue = awardValue;
        }

        public Guid Id { get; private set; }
        public string Description { get; private set; }
        public decimal? AwardValue { get; private set; }
    }
}
