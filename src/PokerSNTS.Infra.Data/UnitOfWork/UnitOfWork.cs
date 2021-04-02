using Microsoft.EntityFrameworkCore;
using PokerSNTS.Domain.Interfaces.UnitOfWork;
using PokerSNTS.Infra.Data.Contexts;
using System;
using System.Threading.Tasks;

namespace PokerSNTS.Infra.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PokerContext _context;

        public UnitOfWork(PokerContext context)
        {
            _context = context;
        }

        public async Task<bool> CommitAsync()
        {
            foreach (var entry in _context.ChangeTracker.Entries())
            {
                if(entry.State == EntityState.Added)
                {
                    entry.Property("Created").CurrentValue = DateTime.Now;
                    entry.Property("Actived").CurrentValue = true;
                }

                if(entry.State == EntityState.Modified)
                {
                    entry.Property("Created").IsModified = false;
                }
            }

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
