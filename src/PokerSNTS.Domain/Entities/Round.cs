using System;

namespace PokerSNTS.Domain.Entities
{
    public class Round : Entity
    {
        public Round(Guid id, string description, DateTime date)
            : base(id)
        {
            Description = description;
            Date = date;
        }

        public string Description { get; private set; }
        public DateTime Date { get; private set; }
    }
}
