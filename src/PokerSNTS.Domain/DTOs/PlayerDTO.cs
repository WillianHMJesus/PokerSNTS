using System;

namespace PokerSNTS.Domain.DTOs
{
    public class PlayerDTO
    {
        public PlayerDTO(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
    }
}
