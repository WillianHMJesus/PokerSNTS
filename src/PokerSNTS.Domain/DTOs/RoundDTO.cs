using System;

namespace PokerSNTS.Domain.DTOs
{
    public class RoundDTO
    {
        public RoundDTO(Guid id, string description, DateTime date, Guid rankingId)
        {
            Id = id;
            Description = description;
            Date = date;
            RankingId = rankingId;
        }

        public Guid Id { get; private set; }
        public string Description { get; private set; }
        public DateTime Date { get; private set; }
        public Guid RankingId { get; private set; }
    }
}
