using Microsoft.EntityFrameworkCore;
using PokerSNTS.Domain.Entities;
using PokerSNTS.Domain.Interfaces.Repositories;
using PokerSNTS.Infra.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerSNTS.Infra.Data.Repositories
{
    public class RankingRepository : IRankingRepository
    {
        private readonly PokerContext _context;

        public RankingRepository(PokerContext context)
        {
            _context = context;
        }

        public void Add(Ranking ranking)
        {
            _context.Ranking.Add(ranking);
        }

        public void Update(Ranking ranking)
        {
            _context.Ranking.Update(ranking);
        }

        public async Task<Ranking> GetByIdAsync(Guid id)
        {
            return await _context.Ranking.FindAsync(id);
        }

        public async Task<IEnumerable<Ranking>> GetAllAsync()
        {
            return await _context.Ranking.AsNoTracking().ToListAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
