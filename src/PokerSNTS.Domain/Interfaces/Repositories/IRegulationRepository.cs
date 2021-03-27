using PokerSNTS.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Interfaces.Repositories
{
    public interface IRegulationRepository
    {
        void Add(Regulation regulation);
        void Update(Regulation regulation);
        Task<IEnumerable<Regulation>> GetAll();
    }
}
