using PokerSNTS.Domain.Entities;
using PokerSNTS.Domain.Interfaces.Repositories;
using PokerSNTS.Infra.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerSNTS.Infra.Data.Repositories
{
    public class RoundRepository : IRoundRepository
    {
        private readonly PokerContext _context;

        public RoundRepository(PokerContext context)
        {
            _context = context;
        }

        public void Add(Round round)
        {
            _context.Rounds.Add(round);
        }

        public void Update(Round round)
        {
            _context.Rounds.Update(round);
        }

        public async Task<IEnumerable<Round>> GetRoundByRankingId(Guid rankingId)
        {
            var ranking = _context.Ranking.Find(rankingId);

            return await Task.FromResult(ranking.Rounds);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
