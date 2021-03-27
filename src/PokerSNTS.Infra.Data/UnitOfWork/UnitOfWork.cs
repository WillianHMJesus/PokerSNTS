using PokerSNTS.Domain.Interfaces.UnitOfWork;
using PokerSNTS.Infra.Data.Contexts;
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

        public async Task<bool> Commit()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
