using PokerSNTS.Domain.Entities;
using PokerSNTS.Domain.Interfaces.Repositories;
using PokerSNTS.Infra.Data.Contexts;
using System;
using System.Threading.Tasks;

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

        public async Task<PlayerRound> GetById(Guid id)
        {
            return await _context.PlayersRounds.FindAsync(id);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
