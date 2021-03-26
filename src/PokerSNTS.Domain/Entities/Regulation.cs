using System;

namespace PokerSNTS.Domain.Entities
{
    public class Regulation : Entity
    {
        public Regulation(Guid id, string description)
            : base(id)
        {
            Description = description;
        }

        public string Description { get; private set; }
    }
}
