using PokerSNTS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerSNTS.Domain.DTOs
{
    public class PlayerDTO
    {
        public PlayerDTO(Player player)
        {
            Id = player.Id;
            Name = player.Name;

            foreach (var playerRound in player.PlayersRounds ?? Enumerable.Empty<PlayerRound>())
            {
                
            }
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public ICollection<PlayerRoundDTO> PlayerRound { get; private set; }
    }
}
