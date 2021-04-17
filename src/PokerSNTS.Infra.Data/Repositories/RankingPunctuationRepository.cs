using Microsoft.EntityFrameworkCore;
using PokerSNTS.Domain.Entities;
using PokerSNTS.Domain.Interfaces.Repositories;
using PokerSNTS.Infra.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerSNTS.Infra.Data.Repositories
{
    public class RankingPointRepository : IRankingPointRepository
    {
        private readonly PokerContext _context;

        public RankingPointRepository(PokerContext context)
        {
            _context = context;
        }

        public void Add(RankingPoint rankingPoint)
        {
            _context.RankingPoints.Add(rankingPoint);
        }

        public void Update(RankingPoint rankingPoint)
        {
            _context.RankingPoints.Update(rankingPoint);
        }

        public async Task<IEnumerable<RankingPoint>> GetAllAsync()
        {
            return await _context.RankingPoints.AsNoTracking().ToListAsync();
        }

        public async Task<RankingPoint> GetByIdAsync(Guid id)
        {
            return await _context.RankingPoints.FindAsync(id);
        }

        public async Task<RankingPoint> GetByPositionAsync(short position)
        {
            return await _context.RankingPoints.AsNoTracking().FirstOrDefaultAsync(x => x.Position == position);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
