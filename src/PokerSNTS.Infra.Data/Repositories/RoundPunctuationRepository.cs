using Microsoft.EntityFrameworkCore;
using PokerSNTS.Domain.Entities;
using PokerSNTS.Domain.Interfaces.Repositories;
using PokerSNTS.Infra.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerSNTS.Infra.Data.Repositories
{
    public class RoundPointRepository : IRoundPointRepository
    {
        private readonly PokerContext _context;

        public RoundPointRepository(PokerContext context)
        {
            _context = context;
        }

        public void Add(RoundPoint roundPoint)
        {
            _context.RoundsPoints.Add(roundPoint);
        }

        public void Update(RoundPoint roundPoint)
        {
            _context.RoundsPoints.Update(roundPoint);
        }

        public async Task<IEnumerable<RoundPoint>> GetAllAsync()
        {
            return await _context.RoundsPoints.AsNoTracking().ToListAsync();
        }

        public async Task<RoundPoint> GetByIdAsync(Guid id)
        {
            return await _context.RoundsPoints.FindAsync(id);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
