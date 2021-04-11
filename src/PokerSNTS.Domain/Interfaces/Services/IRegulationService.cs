using PokerSNTS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Interfaces.Services
{
    public interface IRegulationService
    {
        Task AddAsync(Regulation regulation);
        Task UpdateAsync(Guid id, Regulation regulation);
        Task<IEnumerable<Regulation>> GetAllAsync();
        Task<Regulation> GetByIdAsync(Guid id);
    }
}
