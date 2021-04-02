using PokerSNTS.Domain.Entities;
using System;

namespace PokerSNTS.Domain.DTOs
{
    public class PlayerDTO
    {
        public PlayerDTO(Player player)
        {
            Id = player.Id;
            Name = player.Name;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
    }
}
