using PokerSNTS.Domain.Entities;
using PokerSNTS.Domain.Interfaces.Repositories;
using PokerSNTS.Infra.Data.Contexts;

namespace PokerSNTS.Infra.Data.Repositories
{
    public class PlayerRoundRepository : IPlayerRoundRepository
    {
        private readonly PokerContext _context;

        public PlayerRoundRepository(PokerContext context)
        {
            _context = context;
        }

        public void Add(PlayerRound playerRound)
        {
            _context.PlayersRounds.Add(playerRound);
        }

        public void Update(PlayerRound playerRound)
        {
            _context.PlayersRounds.Update(playerRound);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
