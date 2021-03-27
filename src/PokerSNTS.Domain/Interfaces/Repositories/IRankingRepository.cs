using PokerSNTS.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Interfaces.Repositories
{
    public interface IRankingRepository
    {
        void Add(Ranking ranking);
        void Update(Ranking ranking);
        Task<IEnumerable<Ranking>> GetAll();
    }
}
