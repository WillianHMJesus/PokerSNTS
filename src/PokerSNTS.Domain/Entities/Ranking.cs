using System;

namespace PokerSNTS.Domain.Entities
{
    public class Ranking : Entity
    {
        public Ranking(Guid id, string description, decimal awardValue)
            : base(id)
        {
            Description = description;
            AwardValue = awardValue;
        }
        public string Description { get; private set; }
        public decimal AwardValue { get; private set; }
    }
}
