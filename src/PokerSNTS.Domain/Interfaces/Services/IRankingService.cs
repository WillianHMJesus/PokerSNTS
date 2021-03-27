using PokerSNTS.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Interfaces.Services
{
    public interface IRankingService
    {
        Task<bool> Add(Ranking ranking);
        Task<bool> Update(Ranking ranking);
        Task<IEnumerable<Ranking>> GetAll();
    }
}
