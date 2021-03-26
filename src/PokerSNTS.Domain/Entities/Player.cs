using System;

namespace PokerSNTS.Domain.Entities
{
    public class Player : Entity
    {
        public Player(Guid id, string name)
           : base(id)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}
