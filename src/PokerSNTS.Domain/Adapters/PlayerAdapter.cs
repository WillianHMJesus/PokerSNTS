using PokerSNTS.Domain.DTOs;
using PokerSNTS.Domain.Entities;

namespace PokerSNTS.Domain.Adapters
{
    public class PlayerAdapter
    {
        public static PlayerDTO ToPlayerDTO(Player player)
        {
            return new PlayerDTO(player.Id, player.Name);
        }
    }
}
