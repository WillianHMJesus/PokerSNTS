using Microsoft.EntityFrameworkCore;
using PokerSNTS.Domain.Entities;
using PokerSNTS.Domain.Interfaces.Repositories;
using PokerSNTS.Infra.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerSNTS.Infra.Data.Repositories
{
    public class RegulationRepository : IRegulationRepository
    {
        private readonly PokerContext _context;

        public RegulationRepository(PokerContext context)
        {
            _context = context;
        }

        public void Add(Regulation regulation)
        {
            _context.Regulations.Add(regulation);
        }

        public void Update(Regulation regulation)
        {
            _context.Regulations.Update(regulation);
        }

        public async Task<Regulation> GetById(Guid id)
        {
            return await _context.Regulations.FindAsync(id);
        }

        public async Task<IEnumerable<Regulation>> GetAll()
        {
            return await _context.Regulations.AsNoTracking().ToListAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
