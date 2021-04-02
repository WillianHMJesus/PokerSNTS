using Microsoft.EntityFrameworkCore;
using PokerSNTS.Domain.Entities;
using PokerSNTS.Domain.Interfaces.Repositories;
using PokerSNTS.Infra.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerSNTS.Infra.Data.Repositories
{
    public class RankingPunctuationRepository : IRankingPunctuationRepository
    {
        private readonly PokerContext _context;

        public RankingPunctuationRepository(PokerContext context)
        {
            _context = context;
        }

        public void Add(RankingPunctuation rankingPunctuation)
        {
            _context.RankingPunctuations.Add(rankingPunctuation);
        }

        public void Update(RankingPunctuation rankingPunctuation)
        {
            _context.RankingPunctuations.Update(rankingPunctuation);
        }

        public async Task<RankingPunctuation> GetByIdAsync(Guid id)
        {
            return await _context.RankingPunctuations.FindAsync(id);
        }

        public async Task<IEnumerable<RankingPunctuation>> GetAllAsync()
        {
            return await _context.RankingPunctuations.ToListAsync();
        }

        public async Task<RankingPunctuation> GetRankingPunctuationByPositionAsync(short position)
        {
            return await _context.RankingPunctuations.AsNoTracking().FirstOrDefaultAsync(x => x.Position == position);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
