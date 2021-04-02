using Microsoft.EntityFrameworkCore;
using PokerSNTS.Domain.Entities;
using PokerSNTS.Domain.Interfaces.Repositories;
using PokerSNTS.Infra.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerSNTS.Infra.Data.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly PokerContext _context;

        public PlayerRepository(PokerContext context)
        {
            _context = context;
        }

        public void Add(Player player)
        {
            _context.Players.Add(player);
        }

        public void Update(Player player)
        {
            _context.Players.Update(player);
        }

        public async Task<Player> GetByIdAsync(Guid id)
        {
            return await _context.Players.FindAsync(id);
        }

        public async Task<IEnumerable<Player>> GetAllAsync()
        {
            return await _context.Players.AsNoTracking().ToListAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        } 
    }
}
