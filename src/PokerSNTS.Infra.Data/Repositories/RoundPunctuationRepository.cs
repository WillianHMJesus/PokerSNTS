using Microsoft.EntityFrameworkCore;
using PokerSNTS.Domain.Entities;
using PokerSNTS.Domain.Interfaces.Repositories;
using PokerSNTS.Infra.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerSNTS.Infra.Data.Repositories
{
    public class RoundPunctuationRepository : IRoundPunctuationRepository
    {
        private readonly PokerContext _context;

        public RoundPunctuationRepository(PokerContext context)
        {
            _context = context;
        }

        public void Add(RoundPunctuation roundPunctuation)
        {
            _context.RoundsPunctuations.Add(roundPunctuation);
        }

        public void Update(RoundPunctuation roundPunctuation)
        {
            _context.RoundsPunctuations.Update(roundPunctuation);
        }

        public async Task<IEnumerable<RoundPunctuation>> GetAllAsync()
        {
            return await _context.RoundsPunctuations.AsNoTracking().ToListAsync();
        }

        public async Task<RoundPunctuation> GetByIdAsync(Guid id)
        {
            return await _context.RoundsPunctuations.FindAsync(id);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
