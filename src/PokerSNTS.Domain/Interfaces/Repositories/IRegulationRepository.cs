using PokerSNTS.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Interfaces.Repositories
{
    public interface IRegulationRepository : IRepository<Regulation>
    {
        Task<IEnumerable<Regulation>> GetAllAsync();
    }
}
