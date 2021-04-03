using PokerSNTS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Interfaces.Services
{
    public interface IRegulationService
    {
        Task<Regulation> AddAsync(Regulation regulation);
        Task<Regulation> UpdateAsync(Guid id, Regulation regulation);
        Task<IEnumerable<Regulation>> GetAllAsync();
    }
}
